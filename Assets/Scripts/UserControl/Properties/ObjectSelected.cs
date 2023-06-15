using Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ObjectSelected : SubscribtableStatefull<ISelectable> { }
public class TargetSelected : SubscribtableStateless<IDamagable> { }
public class LeftClickPosition : SubscribtableStateless<Vector3> { }
public class RightClickPosition : SubscribtableStateless<Vector3> { }
