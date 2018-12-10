using UnityEngine;
using System.Collections;
 
public class ObjectivePointer : MonoBehaviour {
  public AsteroidManager asteroidManager;
  public Transform target;
  public Transform skin;

  void Awake () {
    LookForNewTarget();
  }

  void Update () {
    if (target) skin.LookAt(target.position);
    else LookForNewTarget();
  }

  void LookForNewTarget () {
    if (asteroidManager.asteroids.Count > 0) {
      target = asteroidManager.asteroids[0].transform;
    } else {
      Destroy(gameObject);
    }
  }
}