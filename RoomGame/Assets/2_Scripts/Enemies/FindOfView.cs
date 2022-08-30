using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOfView : MonoBehaviour
{
    public struct ViewCastInfo //타겟 정보
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct Edge
    {
        public Vector3 PointA, PointB;
        public Edge(Vector3 _PointA, Vector3 _PointB)
        {
            PointA = _PointA;
            PointB = _PointB;
        }
    }

    public float viewRadius; //사거리
    [Range(0, 360)]
    public float viewAngle; //시야각

    public LayerMask LayerMask_target;  //목표 레이어                //타겟 사이에 다른 오브젝트를 찾고 투과하기 위해
    public LayerMask LayerMask_obstacle;//장애물 레이어   

    public float meshResolution;

    Mesh Mesh_View;
    public MeshFilter MeshFilter_View;

    public bool isEditor = false;

    //시야각 테스트


    private void Start()
    {
        Mesh_View = new Mesh();
        Mesh_View.name = "View Mesh";
        MeshFilter_View.mesh = Mesh_View;

        StartCoroutine(FindTargetsDelay(0.2f));
    }

    private void LateUpdate()
    {
        if (isEditor)
            DrawFindOfView();
    }

    IEnumerator FindTargetsDelay(float delay)//일정 주기동안 타겟을 찾는
    {
        WaitForSeconds delaySc = new WaitForSeconds(delay);

        while(true)
        {        
            FindTargets();
            yield return delaySc;
        }
    }

    void FindTargets() //타겟을 찾는
    {   
        //사거리에 걸리는 모든 콜라이더
        Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, LayerMask_target);
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 targetpos = targets[i].bounds.center; //타겟의 콜라이더 센터 위치 
            Vector3 dirToTarget = (targetpos - transform.position).normalized; //타겟으로의 방향
            if(Vector3.Angle(transform.forward,dirToTarget) < viewAngle/2)  //전방 백터와 타겟방향백터의 크기가 시야각 1/2이면 시야에 들어오는 상태
            {
                float radius = (targets[i] as CapsuleCollider).radius * 0.3f;
                Vector3 pos1 = targetpos + transform.right * radius;
                Vector3 pos2 = targetpos + (-transform.right) * radius;

                Vector3 pos1dir = (pos1 - transform.position).normalized; 
                Vector3 pos2dir = (pos2 - transform.position).normalized; //타겟으로의 방향
                float disToTarget = Vector3.Distance(transform.position, targetpos);// 타겟까지의 거리 계산
                                                                                    
                //타겟의 중앙, 타겟의 좌우방향으로 콜라이거를 한번더 확인하기
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget,LayerMask_obstacle) ||
                    !Physics.Raycast(transform.position, pos1dir, disToTarget, LayerMask_obstacle) ||
                    !Physics.Raycast(transform.position, pos2dir, disToTarget, LayerMask_obstacle)
                    )   //타겟까지 또다른 레이저를 발사
                {//걸리면 장애물이 있다는 소리
                    Debug.Log("타겟 확인");                 
                }
            }
        }     
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, LayerMask_obstacle))
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
    }


    void DrawFindOfView() //시야각 을 보여주는
    {
        //샘플링할 점과 샘플링으로 나눠어지는 각의크기를 구함
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution); //반올림하여 int 형으로 반환
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo prevViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            //i가 0이면 prevViewCast 에  값이 없어 정점 보간을 할수 없으니 건너뛴다.
            if (i != 0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                // 둘 중 한 raycast가 장애물을 만나지 않았거나 두 raycast가 서로 다른 장애물에 hit 된 것이라면(edgeDstThresholdExceed 여부로 계산)
                if (prevViewCast.hit != newViewCast.hit || (prevViewCast.hit && newViewCast.hit && edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewCast);
                    // zero가 아닌 정점을 추가함
                    if (e.PointA != Vector3.zero)
                        viewPoints.Add(e.PointA);                

                    if (e.PointB != Vector3.zero)                
                        viewPoints.Add(e.PointB);         
                }
            }

            viewPoints.Add(newViewCast.point);
            prevViewCast = newViewCast;

        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
  
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        Mesh_View.Clear();
        Mesh_View.vertices = vertices;
        Mesh_View.triangles = triangles;
        Mesh_View.RecalculateNormals();
    }


    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal) ///y축 오일러 각을 3차원 방향 벡터로 변환한다.
    {
        if (!angleIsGlobal)    
            angleDegrees += transform.eulerAngles.y;
        

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }

    public int edgeResolveIterations = 100;
    public float edgeDstThreshold;
    Edge FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = minAngle + (maxAngle - minAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceed = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if(newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceed)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new Edge(minPoint, maxPoint);
    }

}
