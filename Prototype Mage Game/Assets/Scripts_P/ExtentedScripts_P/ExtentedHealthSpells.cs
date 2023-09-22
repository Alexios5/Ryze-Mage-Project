using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using MoreMountains.CorgiEngine;



public class ExtentedHealthSpells : Health
{
	public override void Kill()
	{
		if (ImmuneToDamage)
		{
			return;
		}

		if (_character != null)
		{
			// we set its dead state to true
			_character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Dead);
			_character.Reset();

			if (_character.CharacterType == Character.CharacterTypes.Player)
			{
				CorgiEngineEvent.Trigger(CorgiEngineEventTypes.PlayerDeath, _character);
			}
		}
		SetHealth(0f, this.gameObject);

		// we prevent further damage
		DamageDisabled();

		// instantiates the destroy effect
		DeathFeedbacks?.PlayFeedbacks();

		// Adds points if needed.
		if (PointsWhenDestroyed != 0)
		{
			// we send a new points event for the GameManager to catch (and other classes that may listen to it too)
			CorgiEnginePointsEvent.Trigger(PointsMethods.Add, PointsWhenDestroyed);
		}

		if (_animator != null)
		{
			_animator.SetTrigger("Death");
		}

		if (OnDeath != null)
		{
			OnDeath();
		}

		HealthDeathEvent.Trigger(this);

		// if we have a controller, removes collisions, restores parameters for a potential respawn, and applies a death force
		if (_controller != null)
		{
			// we make it ignore the collisions from now on
			if (CollisionsOffOnDeath)
			{
				_controller.CollisionsOff();
				if (_collider2D != null)
				{
					_collider2D.enabled = false;
				}
			}

			// we reset our parameters
			_controller.ResetParameters();

			if (GravityOffOnDeath)
			{
				_controller.GravityActive(false);
			}

			// we reset our controller's forces on death if needed
			if (ResetForcesOnDeath)
			{
				_controller.SetForce(Vector2.zero);
			}

			// we apply our death force
			if (ApplyDeathForce)
			{
				_controller.GravityActive(true);
				_controller.SetForce(DeathForce);
			}
		}


		// if we have a character, we want to change its state
		if (_character != null)
		{
			// we set its dead state to true
			_character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Dead);
			_character.Reset();

			// if this is a player, we quit here
			if (_character.CharacterType == Character.CharacterTypes.Player)
			{
				return;
			}
		}

		if (DelayBeforeDestruction > 0f)
		{
			//this.gameObject.MMGetComponentNoAlloc<Projectile>().Speed = 0f;
			Invoke("DestroyObject", DelayBeforeDestruction);
		}
		else
		{
			// finally we destroy the object
			DestroyObject();
		}
	}
}
