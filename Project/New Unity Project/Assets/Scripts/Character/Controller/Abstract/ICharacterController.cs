using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController
{
    public float Speed { get; set; }

    public void Move();
}
