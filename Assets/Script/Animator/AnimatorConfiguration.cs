using UnityEngine;
using System.Collections;

public static class AnimatorConfig
{
	//layer
	public const string LAYER_BASE = "Base Layer.";
	public const string LAYER_LOCOMOTION = "Locomotion.";
	public const string LAYER_COMBO = "Combo.";
	
	//state
	public const string STATE_IDLE = "Idle";
	public const string STATE_MOVE = "Move";
	public const string STATE_FIST_BASE = "Fist_Base";
	public const string STATE_KICK_BASE = "Kick_Base";
	public const string STATE_KICK_THRUST = "Kick_Thrust";
	public const string STATE_KICK_SPIN = "Kick_Spin";
	public const string STATE_KICK_ROUND = "Kick_Round";
	public const string STATE_RISING_DRAGON = "Rising_Dragon";
	
	//param
	public const string PARAM_FLOAT_MOVE_SPEED = "move_speed";
	public const string PARAM_TRIGGER_FIST_BASE = "fist_base";
	public const string PARAM_TRIGGER_KICK_BASE = "kick_base";
	public const string PARAM_TRIGGER_KICK_THRUST = "kick_thrust";
	public const string PARAM_TRIGGER_KICK_SPIN = "kick_spin";
	public const string PARAM_TRIGGER_KICK_ROUND = "kick_round";
	public const string PARAM_TRIGGER_RISING_DRAGON = "rising_dragon";
	public const string PARAM_TRIGGER_JUMP_START = "jump_start";
	public const string PARAM_TRIGGER_HIT = "hit";
	public const string PARAM_BOOL_IN_AIR = "in_air";	


}
