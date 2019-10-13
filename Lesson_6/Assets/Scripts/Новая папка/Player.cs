using UnityEngine;
namespace Geekbrains
{
    public class Player : BaseObjectScene//, IHeal
    {
        public Sprite icon;
        public string description = "Описание";
        public float minHealth = 0;
        public float maxHelth = 100;
        public bool rangedUnit;
        public int rangedAttack = 100;
        public float damage = 10;
    }
}
