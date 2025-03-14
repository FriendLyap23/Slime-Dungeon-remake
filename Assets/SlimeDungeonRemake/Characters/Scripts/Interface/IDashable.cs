using System.Collections;
using UnityEngine;

public interface IDashable 
{
    public bool _canDash { get; set; }
    public bool _isDashing { get; set; }

    public float _dashingPower { get; set; }
    public float _dasgingTime { get; set; }
    public float _dashingCooldown { get; set; }

    public TrailRenderer _trailRenderer { get; set; }

    public IEnumerator DashCharacter();
}
