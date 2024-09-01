using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace Enemy.JungleKid
{
    public class JKAnimation : MonoBehaviour
    {
        public JKMain jk;

        public StateMachine CurrentFSM;
        public StateMachine CommonFSM;
        public StateMachine FollowFSM;
        public Rigidbody2D rb;

        private void Start()
        {
            InitFSM();
            jk = GetComponent<JKMain>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CurrentFSM.Excute();
        }

        private void InitFSM()
        {
            
            
        }
    }
}
