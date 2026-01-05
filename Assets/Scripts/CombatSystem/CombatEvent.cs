namespace Study_Camera.CombatSystem
{
    public struct CombatEvent
    {
        public ICombatAgent Sender;
        public ICombatAgent Receiver;
        
        public int Damage;
        public HitInfo HitInfo;
    }
}