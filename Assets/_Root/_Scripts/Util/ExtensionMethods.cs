using UnityEngine;

namespace Hullbreakers
{
    public static class ExtensionMethods
    {
        public const int PLAYER_ATTACKS_LAYER = 14;
        public const int PLAYER_LAYER = 10, DRONE_LAYER = 9;

        public const int ENEMY_ATTACKS_LAYER = 15;
        public const int ENEMY_LAYER = 11;

        public static readonly LayerMask PlayerMask = Physics2D.GetLayerCollisionMask(ENEMY_LAYER), EnemyMask = Physics2D.GetLayerCollisionMask(PLAYER_LAYER);

        public const float TWO_PI = 2f * Mathf.PI;
        const float k_TwoThirdsPi = TWO_PI / 3f;
        const float k_FourThirdsPi = 2f * k_TwoThirdsPi;
        

        public static bool AmPlayerAttack(this Component component)
        {
            return component.gameObject.layer == PLAYER_ATTACKS_LAYER;
        }

        public static bool AmPlayer(this Component component)
        {
            return component.gameObject.layer is PLAYER_LAYER or DRONE_LAYER;
        }

        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void AddLayer(this ref LayerMask originalLayerMask, int layerToAdd)
        {
            originalLayerMask |= (1 << layerToAdd);
        }

        public static void RemoveLayer(this ref LayerMask originalLayerMask, int layerToRemove)
        {
            originalLayerMask &= ~(1 << layerToRemove);
        }

        public static Vector2 DirectionFromAngleDegrees(this float angle)
        {
            return DirectionFromAngle(angle * Mathf.Deg2Rad);
        }

        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = Random.Range(0, n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static Vector2 DirectionFromAngle(this float angle)
        {
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }

        public static Vector2 PosInCircle(float radius, int index, int count)
        {
            float radians = (float)index / count * TWO_PI;
            return new Vector2(radius * Mathf.Cos(radians), radius * Mathf.Sin(radians));
        }

        public static float AngleFromDirection(this Vector2 pos)
        {
            pos.Normalize();
            return Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        }

        public static void OrientAlongVelocity(this Rigidbody2D rb)
        {
            if (rb.velocity == Vector2.zero) return;
            rb.rotation = rb.velocity.AngleFromDirection();
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static Color Complementary(this Color color)
        {
            Color returned = Color.white - color;
            returned.a = color.a;
            return returned;
        }

        public static Color rainbow {
            get
            {
                float time = Time.time;

                return new Color(
                    Mathf.Sin(time).Remap(-1, 1, 0, 1),
                    Mathf.Sin(time + k_TwoThirdsPi).Remap(-1, 1, 0, 1),
                    Mathf.Sin(time + k_FourThirdsPi).Remap(-1, 1, 0, 1)
                );
            }
        }
        
        public static Color SetRGB(this Color color, Color replacement)
        {
            return new Color(replacement.r, replacement.g, replacement.b, color.a);
        }

        public static Quaternion RotationFromDirection2D(this Vector2 direction)
        {
            return Quaternion.AngleAxis(direction.AngleFromDirection(), Vector3.forward);
        }

        public static Quaternion RotationFromAngle2D(this float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        const float k_Dmgmin = 0f, k_Dmgmax = 100f;
        const float k_Scalemin = 0.5f, k_Scalemax = 4f;
        
        public static float CalculateFXScale(this float resultDmg)
        {
            return resultDmg.Remap(k_Dmgmin, k_Dmgmax, k_Scalemin, k_Scalemax);
        }
    }
}
