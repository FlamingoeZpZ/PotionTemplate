using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Optimization.Baseline
{
    /*
     * Cannon will shoot whenever it can shoot
     * Cannon will shoot a random projectile based on a set
     */
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Projectile[] projectilesPrefabs;
        
        [SerializeField, Min(0)] private float minShootTime;
        [SerializeField, Min(0)] private float maxShootTime;
        [SerializeField, Min(0)] private float minForceModifier;
        [SerializeField, Min(0)] private float maxForceModifier;
        [SerializeField, Range(0, 90)] private float deviationDegrees;
        
        [SerializeField, Min(1)] private int minProjectiles;
        [SerializeField, Min(1)] private int maxProjectiles;
        
        private float _currentTime;
        void Awake()
        {
            ResetCannon();
        }
        void Update()
        { 
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0)
            {
                Shoot();
            }
        }
        void Shoot()
        {
            int n = Random.Range(minProjectiles, maxProjectiles);
            for (int i = 0; i < n; i++)
            {
                Projectile p = Instantiate(projectilesPrefabs[Random.Range(0, projectilesPrefabs.Length)], transform.position, transform.rotation);
                float forceModifier = Random.Range(minForceModifier, maxForceModifier);
                float xDeviation = Random.Range(-deviationDegrees, deviationDegrees);
                float yDeviation = Random.Range(-deviationDegrees, deviationDegrees);

                Vector3 direction = Quaternion.AngleAxis(yDeviation, Vector3.right) * (Quaternion.AngleAxis(xDeviation, Vector3.up) * transform.forward);
                
                p.Launch(direction * forceModifier);
            }


            ResetCannon();
        }

        void ResetCannon()
        {
            _currentTime = Random.Range(minShootTime, maxShootTime);
        }
    }
}
