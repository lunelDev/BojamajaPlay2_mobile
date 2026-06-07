using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AnalogFlick : MonoBehaviour
{
  // How many segments to divide the ring into.
  const int BUCKET_COUNT = 8;

  // Data structure to define combos.
  // You can add to this things like animations, point values, etc.
  // Or make it a ScriptableObject that can live as a separate asset.
  [System.Serializable]
  public class Combo
  {
    public string name;
    public string[] patterns;
  }

  // Gesture detection parameters.
  public float deadzoneRadius = 0.2f;
  public float activationRadius = 0.8f;
  public float maxGestureDuration = 1f;

  public Combo[] combos;

  Dictionary<string, Combo> _patternMatch = new Dictionary<string, Combo>();
  StringBuilder _progress = new StringBuilder();

  float _gestureDuration = 0f;
  [SerializeField]
  int _lastBucket;
  int _framesInDeadzone;
  bool _readyToBegin;

  string _patternInProgress = "";



  void Start()
  {
    // Convert our Inspector list of combos into a dictionary for quick string lookups.
    foreach (var combo in combos)
    {
      foreach (var pattern in combo.patterns)
        _patternMatch.Add(pattern, combo);
    }
  }

  // Update is called once per frame
  void Update()
  {
    // Collect our input and magnitude.
    Vector2 stick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

#if UNITY_EDITOR
    _analogHistory[Time.frameCount & 31] = stick;
#endif

    float magnitude = stick.magnitude;

    // If we're mid-gesture and cross within the deadzone, the flick might be over.
    if (magnitude < deadzoneRadius)
    {
      // Give some tolerance to cross the deadzone during a zig-zag, before we cut off.
      if (_gestureDuration > 0f && _framesInDeadzone > 2)
        EndFlick();

      //Debug.Log("Grazed deadzone");
      _framesInDeadzone++;
      // We've returned to neutral, so we can start a new gesture if we're not already in one.
      _readyToBegin = true;
    }
    else
    {
      // Otherwise, reset our deadzone watch.
      _framesInDeadzone = 0;

      // If we've deflected far enough to start a flick...
      if (_readyToBegin == true && magnitude > activationRadius)
      {
        // Start a new flick if we're not already.
        if (_gestureDuration == 0f)
          BeginFlick();

        // Abort the gesture if it's gone too long.
        _gestureDuration += Time.deltaTime;
        if (_gestureDuration > maxGestureDuration)
        {
          EndFlick();

          // Finally, try to progress the gesture using this input.
        }
        else if (ProgressFlick(stick))
        {
          // If we've completed a valid pattern, fire the combo and end the gesture.
          if (CheckPattern())
            EndFlick();
        }
      }
    }
  }

  // Initialize flick state.
  void BeginFlick()
  {
    _progress.Length = 0;
    _lastBucket = -1;
  }

  // Try appending a new entry to the combo.
  bool ProgressFlick(Vector2 stick)
  {
    const float BUCKETS_PER_RADIAN = BUCKET_COUNT / (2 * Mathf.PI);

    // Get the angle of the stick, and round it to one of a fixed number of buckets.
    float angle = Mathf.Atan2(-stick.x, -stick.y);
    int bucket = Mathf.RoundToInt(angle * BUCKETS_PER_RADIAN + BUCKET_COUNT / 2.0f) % BUCKET_COUNT;
    // If we've changed buckets, add a new letter to our combo.
    if (bucket != _lastBucket)
    {
      _progress.Append((char)('A' + bucket));
      _lastBucket = bucket;
      return true;
    }
    // Otherwise, nothing new to report.
    return false;
  }

  bool CheckPattern()
  {
    // Read out our pattern so far...
    _patternInProgress = _progress.ToString();

    // Check whether it matches any of our combos.
    if (_patternMatch.TryGetValue(_patternInProgress, out Combo combo))
    {
      Debug.LogFormat("Combo: {0} ({1})", combo.name, _patternInProgress);
      // TODO: Do the combo's trick!

      _patternInProgress = "";
      return true;
    }

    return false;
  }

  void EndFlick()
  {
    // For debugging, it's useful to know what the algorithm "thought" you entered.
    if (_patternInProgress != "")
      Debug.LogFormat("Unknown Combo: {0}", _patternInProgress);

    // Mark the gesture as over.
    _gestureDuration = 0f;
    // But don't let us start a new gesture till we return to neutral.
    _readyToBegin = false;
    _patternInProgress = "";
  }

#if UNITY_EDITOR
  Vector2[] _analogHistory = new Vector2[32];
  private void OnDrawGizmos()
  {
    Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 1, 0));
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(Vector3.zero, 1.0f);
    Gizmos.color = Color.grey;
    Gizmos.DrawWireSphere(Vector3.zero, activationRadius);
    Gizmos.color = Color.black;
    Gizmos.DrawWireSphere(Vector3.zero, deadzoneRadius);

    Gizmos.color = Color.grey;
    for (int i = 0; i < BUCKET_COUNT; i++)
    {
      Vector3 direction = Quaternion.Euler(0, 0, (360.0f / BUCKET_COUNT) * (i + 0.5f)) * Vector3.up;
      Gizmos.DrawLine(1f * direction, activationRadius * direction);
    }

    Gizmos.color = _gestureDuration == 0f ? Color.blue : Color.red;
    Vector3 lastPosition = _analogHistory[(Time.frameCount + 1) & 31];
    for (int x = 2; x <= 32; x++)
    {
      var next = _analogHistory[(Time.frameCount + x) & 31];
      Gizmos.DrawLine(lastPosition, next);
      lastPosition = next;
    }

    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(lastPosition, 0.1f);
  }
#endif
}