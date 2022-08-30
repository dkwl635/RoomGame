using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOfView : MonoBehaviour
{
    public struct ViewCastInfo //Ÿ�� ����
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

    public float viewRadius; //��Ÿ�
    [Range(0, 360)]
    public float viewAngle; //�þ߰�

    public LayerMask LayerMask_target;  //��ǥ ���̾�                //Ÿ�� ���̿� �ٸ� ������Ʈ�� ã�� �����ϱ� ����
    public LayerMask LayerMask_obstacle;//��ֹ� ���̾�   

    public float meshResolution;

    Mesh Mesh_View;
    public MeshFilter MeshFilter_View;

    public bool isEditor = false;

    //�þ߰� �׽�Ʈ


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

    IEnumerator FindTargetsDelay(float delay)//���� �⵿ֱ�� Ÿ���� ã��
    {
        WaitForSeconds delaySc = new WaitForSeconds(delay);

        while(true)
        {        
            FindTargets();
            yield return delaySc;
        }
    }

    void FindTargets() //Ÿ���� ã��
    {   
        //��Ÿ��� �ɸ��� ��� �ݶ��̴�
        Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, LayerMask_target);
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 targetpos = targets[i].bounds.center; //Ÿ���� �ݶ��̴� ���� ��ġ 
            Vector3 dirToTarget = (targetpos - transform.position).normalized; //Ÿ�������� ����
            if(Vector3.Angle(transform.forward,dirToTarget) < viewAngle/2)  //���� ���Ϳ� Ÿ�ٹ�������� ũ�Ⱑ �þ߰� 1/2�̸� �þ߿� ������ ����
            {
                float radius = (targets[i] as CapsuleCollider).radius * 0.3f;
                Vector3 pos1 = targetpos + transform.right * radius;
                Vector3 pos2 = targetpos + (-transform.right) * radius;

                Vector3 pos1dir = (pos1 - transform.position).normalized; 
                Vector3 pos2dir = (pos2 - transform.position).normalized; //Ÿ�������� ����
                float disToTarget = Vector3.Distance(transform.position, targetpos);// Ÿ�ٱ����� �Ÿ� ���
                                                                                    
                //Ÿ���� �߾�, Ÿ���� �¿�������� �ݶ��̰Ÿ� �ѹ��� Ȯ���ϱ�
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget,LayerMask_obstacle) ||
                    !Physics.Raycast(transform.position, pos1dir, disToTarget, LayerMask_obstacle) ||
                    !Physics.Raycast(transform.position, pos2dir, disToTarget, LayerMask_obstacle)
                    )   //Ÿ�ٱ��� �Ǵٸ� �������� �߻�
                {//�ɸ��� ��ֹ��� �ִٴ� �Ҹ�
                    Debug.Log("Ÿ�� Ȯ��");                 
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


    void DrawFindOfView() //�þ߰� �� �����ִ�
    {
        //���ø��� ���� ���ø����� ���������� ����ũ�⸦ ����
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution); //�ݿø��Ͽ� int ������ ��ȯ
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo prevViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            //i�� 0�̸� prevViewCast ��  ���� ���� ���� ������ �Ҽ� ������ �ǳʶڴ�.
            if (i != 0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                // �� �� �� raycast�� ��ֹ��� ������ �ʾҰų� �� raycast�� ���� �ٸ� ��ֹ��� hit �� ���̶��(edgeDstThresholdExceed ���η� ���)
                if (prevViewCast.hit != newViewCast.hit || (prevViewCast.hit && newViewCast.hit && edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewCast);
                    // zero�� �ƴ� ������ �߰���
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


    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal) ///y�� ���Ϸ� ���� 3���� ���� ���ͷ� ��ȯ�Ѵ�.
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
