

namespace MiniGameHelper
{
    public delegate void QuestEvent();

    interface MiniGame
    {
        void QuestSetFunc(QuestEvent ClearFunc , QuestEvent CloesFunc);
        void MiniGameStart();
        void MiniGameClear();

    }
}
