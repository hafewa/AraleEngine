﻿using UnityEngine;
using System.Collections;
using Arale.Engine;
using UnityEngine.EventSystems;

public class BattleSceneCtrl : SceneCtrl {
	public Unit player{ get; protected set;}
	protected override void onAwake()
	{
		EventMgr.single.AddListener ("Game.Player", OnBindPlayer);
	}

	protected override void onDestroy()
	{
		EventMgr.single.RemoveListener ("Game.Player", OnBindPlayer);
	}

	protected override void onUpdate()
	{
		//点中ui
		bool hitUI = (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject () && (!UIStick.isHit));
		if (player == null)return;
		if (Input.GetMouseButtonDown (0))
		{//行走目标选择
			if(hitUI)return;
			if(GUIUtility.hotControl!=0)return;//点在GUI上了
			if(Camera.main==null)return;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, (1<<LayerMask.NameToLayer("Client")|1<<LayerMask.NameToLayer("Ground"))))
			{
				if (hit.collider.name == "NavMesh")
				{
                    player.move.nav(hit.point);
				}
				else
				{
					Unit u = hit.collider.gameObject.GetComponent<Unit> ();
					if (u != null && u.type == UnitType.Drop)
					{
						(u as DropItems).pick (0);
					}
				}
			}
		}

		if (Input.GetMouseButtonDown (1))
		{//技能目标选择
			if(hitUI)return;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, float.MaxValue, (1<<LayerMask.NameToLayer("Client")|1<<LayerMask.NameToLayer("Ground"))))
			{
				Unit u = hit.collider.gameObject.GetComponent<Unit> ();
				if (u != null)
				{
					player.skill.targetPos  = u.pos;
					player.skill.targetUnit = u;
				}
				else if(hit.collider.gameObject.name == "NavMesh")
				{
					player.skill.targetPos = hit.point;
				}
				player.forward (player.skill.targetPos);
				player.addState (0, true);
			}
		}

		if(Input.GetKeyDown (KeyCode.Space)) {
			player.move.jump ();
		}
		if (Input.GetKeyDown (KeyCode.F1)) {
			player.skill.playIndex(0);
		}
		if (Input.GetKeyDown (KeyCode.F2)) {
			player.skill.playIndex(1);
		}
		if (Input.GetKeyDown (KeyCode.F3)) {
			player.skill.playIndex(2);
		}
		if (Input.GetKeyDown (KeyCode.F4)) {
			player.skill.playIndex(3);
		}
	}

	void OnBindPlayer(EventMgr.EventData ed)
	{
		player = ed.data as Player;
	}
}
