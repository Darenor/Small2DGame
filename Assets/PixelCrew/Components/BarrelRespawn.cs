using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PixelCrew.Components
{
    public class BarrelRespawn : MonoBehaviour
    {
        private Vector3 startPos;

                
        public float threshold;

         void FixedUpdate()
         {
                if (transform.position.y < threshold)
                    transform.position = new Vector3(2, -0.51f, 0);
         }
        
    }
        
}


