using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using DSA.Extensions.Base;

namespace DSA.Extensions.GameTime
{
	public class DailyTextureOffsetSystem : ComponentSystem
	{
		public struct OffsetGroup
		{
			public MeshRenderer Renderer;

			public DailyUpdateComponent DailyOffset;

			public VariableTextureOffsetComponent VariableOffset;
		}

		private float2 secondOffset;
		private float2 startPoint;
		float timeScale;
		[Inject] ClockSystem clockSystem;

		protected override void OnCreateManager()
		{
			EntityManager entityManager = World.GetOrCreateManager<EntityManager>();
		}

		protected override void OnUpdate()
		{
			foreach (var entity in GetEntities<OffsetGroup>())
			{
				timeScale = clockSystem.DailySeconds;
				if (clockSystem.DailySeconds < 43200)
				{
					secondOffset = (entity.VariableOffset.max - entity.VariableOffset.min) / 12;
					startPoint = entity.VariableOffset.min;
				}
				else
				{
					secondOffset = (entity.VariableOffset.min - entity.VariableOffset.max) / 12;
					startPoint = entity.VariableOffset.max;
					timeScale -= 43200;
				}
				secondOffset = secondOffset / 60;
				secondOffset = secondOffset / 60;
				entity.Renderer.materials[entity.VariableOffset.materialIndex].mainTextureOffset = startPoint + timeScale * secondOffset;
			}
		}
	}
}
