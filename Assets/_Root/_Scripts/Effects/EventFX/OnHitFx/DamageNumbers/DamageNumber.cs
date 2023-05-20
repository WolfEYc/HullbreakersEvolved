using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TMP_Text), typeof(Rigidbody2D))]
    public class DamageNumber : TimedPooledObject<DamageNumber>
    {
        TMP_Text _text;
        Rigidbody2D _rb;

        const float k_Deviation = 0.5f;
        const float k_Speed = 1f;


        const float k_DmgMin = 0, k_DmgMax = 1000;
        const float k_MinScale = 1f, k_MaxScale = 2f;
        

        const float k_MinTtl = 0.5f, k_MaxTtl = 2f;
        

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<TMP_Text>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public override float sizeRadius => Transform.localScale.x;

        void CalcScale(float dmg)
        {
            Transform.localScale = Vector2.one * dmg.Remap(k_DmgMin, k_DmgMax, k_MinScale, k_MaxScale);
        }

        void CalcTtl(float dmg)
        {
            Timer.StartCountdown(dmg.Remap(k_DmgMin, k_DmgMax, k_MinTtl, k_MaxTtl));
        }

        void CalcVelocity(Vector2 dir)
        {
            dir.x += Random.Range(-k_Deviation, k_Deviation);
            dir.y += Random.Range(-k_Deviation, k_Deviation);
            _rb.velocity = dir * k_Speed;
        }

        public void Deploy(int dmg, Vector2 pos, Vector2 dir)
        {
            Transform.position = pos;
            
            CalcVelocity(dir);
            CalcScale(dmg);
            CalcTtl(dmg);
            
            _text.SetText(dmg.ToString());
        }
        
        
    }
}
