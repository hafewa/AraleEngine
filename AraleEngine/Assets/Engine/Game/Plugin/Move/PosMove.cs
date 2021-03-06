﻿using UnityEngine;
using System.Collections;

public class PosMove : Move
{
	protected override void start(Unit unit)
	{
		mSpeed = table.speed;
		if(mSpeed == 0)
		{//直接放置目的地
            unit.dir = (vTarget - unit.pos).normalized;
            unit.pos = vTarget;
            stop(unit,true);
		}
	}


	protected override void update(Unit unit)
	{
        Vector3 dv = vTarget - unit.pos;
		if (dv.sqrMagnitude <= mSpeed*mSpeed)
		{//达到目的地
            unit.pos = vTarget;
            stop(unit,true);
		}
		else
		{
			unit.pos += dv.normalized* mSpeed;
		}
	}
}
