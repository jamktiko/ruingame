using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector3 _directionInput { get;  protected set; }
    public bool sprintInput { get;  protected set;  }
    public bool dodgeInput { get; protected set; }
    public bool attackInput { get;  protected set; }
    
    
    public virtual void OnEnable()
    {
        
    }

    public virtual void OnDisable()
    {
        
    }
}