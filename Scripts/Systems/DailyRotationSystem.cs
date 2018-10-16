using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using DSA.Extensions.Base;

namespace DSA.Extensions.GameTime
{
	public class DailyRotationSystem : ComponentSystem
	{

		public struct DailyRotationGroup
		{
			public Transform Transform;

			// public Rotation Rotation;

			public DailyUpdateComponent DailyRotationComponent;

			public VariableRotationComponent VariableRotationComponent;
		}

		private float secondRotation;
		[Inject] ClockSystem clockSystem;

		protected override void OnCreateManager()
		{
			EntityManager entityManager = World.GetOrCreateManager<EntityManager>();
			secondRotation = (360 / 24);
			secondRotation = secondRotation / 60;
			secondRotation = secondRotation / 60;
		}

		protected override void OnUpdate()
		{
			float rotationAngle = clockSystem.DailySeconds * secondRotation;
			foreach (var entity in GetEntities<DailyRotationGroup>())
			{
				entity.Transform.rotation = Quaternion.Euler(entity.VariableRotationComponent.minValue) * Quaternion.AngleAxis(clockSystem.DailySeconds * secondRotation, entity.VariableRotationComponent.rotationAxis);
			}
		}

		// [BurstCompile]
		// struct DailyRotation : IJobProcessComponentData<Rotation, VariableRotationData>
		// {
		// 	public float rotationScale;

		// 	public void Execute(ref Rotation rotation, [ReadOnly]ref VariableRotationData rotationData)
		// 	{
		// 		// Quaternion.Euler(entity.VariableRotationComponent.minValue) * Quaternion.AngleAxis(dailySeconds * secondRotation, entity.VariableRotationComponent.rotationAxis)
		// 		rotation.Value = Quaternion.Euler(rotationData.minValue) * Quaternion.AngleAxis(rotationScale, rotationData.rotationAxis);
		// 	}
		// }

		// protected override JobHandle OnUpdate(JobHandle inputDeps)
		// {
		// 	dailySeconds = clockSystem.TimeCounter;
		// 	// Debug.Log(dailySeconds);
		// 	if (dailySeconds > 86400) { dailySeconds -= 86400; }
		// 	float rotationAngle = dailySeconds * secondRotation;
		// 	var job = new DailyRotation() { rotationScale = dailySeconds };
		// 	return job.Schedule(this, inputDeps);
		// }
	}
}