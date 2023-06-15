using Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelected : ReactivePropertyAsync<ISelectable> { }
public class TargetSelected : ReactivePropertyAsync<IDamagable> { }
public class LeftClickPosition : ReactivePropertyAsync<Vector3> { }
public class RightClickPosition : ReactivePropertyAsync<Vector3> { }
