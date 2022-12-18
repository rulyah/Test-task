using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    public class GameConfig : MonoBehaviour
    {
        public static GameConfig instance { get; private set; }

        public List<Transform> wayPoints;
        public int maxShipsCount = 8;
        public float bulletSpeed = 4000.0f;
        public int minBulletDamage = 1;
        public int maxBulletDamage = 3;
        public float despawnDepth = -20.0f;
        public float reloadTime = 2.0f;
        public float prepareForShooting = 0.3f;
        public float gunRotationSpeed = 0.5f;
        public float cameraSensitivity = 15.0f;
        public float cameraSmooth = 0.1f;
        public int maxShipHealth = 3;
        public float maxShipSpeed = 5;
        public float cameraVerticalMin = -10.0f;
        public float cameraVerticalMax = 10.0f;

        private void Awake()
        {
            instance = this;
        }
    }
}