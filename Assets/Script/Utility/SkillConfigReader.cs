using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Skill
{
	public string Id_; // unique name for identifying
	public string AnimatorParam_; // trigger param in AnimatorController
	public string AnimatorState_; // action state name in AnimatorController
	public string AntecedentComboId_; // trigger after another skill 
	public int ComboTriggerFrameBegin_; // begin frame of the antecedent skill
	public int ComboTriggerFrameEnd_; // end frame of the antecedent skill
}

public class SkillConfigReader
{
	static string path = "Config/SkillConfig";

	public static List<Skill> m_CacheData = null;

	public static void InitRead ()
	{
		m_CacheData = new List<Skill> ();

		XmlDocument xmlDoc = new XmlDocument ();
		string data = Resources.Load(path).ToString ();
		xmlDoc.LoadXml (data);
		
		XmlElement rootElem = xmlDoc.DocumentElement;   //获取根节点  
		XmlNodeList nodes = rootElem.GetElementsByTagName("item"); //获取tile子节点集合 
		
		foreach (XmlNode node in nodes)
		{  
			Skill item = new Skill ();
			
			item.Id_ = ((XmlElement)node).GetAttribute("id");
			item.AnimatorParam_ = ((XmlElement)node).GetAttribute("param");
			item.AnimatorState_ = ((XmlElement)node).GetAttribute("state");
			item.AntecedentComboId_ = ((XmlElement)node).GetAttribute("comboId");
			item.ComboTriggerFrameBegin_ = int.Parse( ((XmlElement)node).GetAttribute("comboFrameBegin") );
			item.ComboTriggerFrameEnd_ = int.Parse( ((XmlElement)node).GetAttribute("comboFrameEnd") );
			
			m_CacheData.Add (item);
		}
	}

	public static Skill GetSkillById (string _id, bool readFromCache = true)
	{
		Skill ret = null;

		if (null != m_CacheData && readFromCache) 
		{
			for (int i=0; i<m_CacheData.Count; ++i)
			{
				if ( _id.Equals (m_CacheData[i].Id_))
				{
					return m_CacheData[i];
				}
			}
		}

		XmlDocument xmlDoc = new XmlDocument ();
		string data = Resources.Load(path).ToString ();
		xmlDoc.LoadXml (data);

		XmlElement rootElem = xmlDoc.DocumentElement;   //获取根节点  
		XmlNodeList nodes = rootElem.GetElementsByTagName("item"); //获取tile子节点集合 
		
		foreach (XmlNode node in nodes)
		{  
			if ( _id.Equals (((XmlElement)node).GetAttribute("id")))
			{
				Skill item = new Skill ();
				
				item.Id_ = ((XmlElement)node).GetAttribute("id");
				item.AnimatorParam_ = ((XmlElement)node).GetAttribute("param");
				item.AnimatorState_ = ((XmlElement)node).GetAttribute("state");
				item.AntecedentComboId_ = ((XmlElement)node).GetAttribute("comboId");
				item.ComboTriggerFrameBegin_ = int.Parse( ((XmlElement)node).GetAttribute("comboFrameBegin") );
				item.ComboTriggerFrameEnd_ = int.Parse( ((XmlElement)node).GetAttribute("comboFrameEnd") );
				
				ret = item;
				
				break;
			}
		}
		
		return ret;
	}

	public static Skill GetSkillByState (string _state, bool readFromCache = true)
	{
		Skill ret = null;
		
		if (null != m_CacheData && readFromCache) 
		{
			for (int i=0; i<m_CacheData.Count; ++i)
			{
				if ( _state.Equals (m_CacheData[i].AnimatorState_))
				{
					return m_CacheData[i];
				}
			}
		}
		
		XmlDocument xmlDoc = new XmlDocument ();
		string data = Resources.Load(path).ToString ();
		xmlDoc.LoadXml (data);
		
		XmlElement rootElem = xmlDoc.DocumentElement;   //获取根节点  
		XmlNodeList nodes = rootElem.GetElementsByTagName("item"); //获取tile子节点集合 
		
		foreach (XmlNode node in nodes)
		{  
			if ( _state.Equals (((XmlElement)node).GetAttribute("state")))
			{
				Skill item = new Skill ();
				
				item.Id_ = ((XmlElement)node).GetAttribute("id");
				item.AnimatorParam_ = ((XmlElement)node).GetAttribute("param");
				item.AnimatorState_ = ((XmlElement)node).GetAttribute("state");
				item.AntecedentComboId_ = ((XmlElement)node).GetAttribute("comboId");
				item.ComboTriggerFrameBegin_ = int.Parse( ((XmlElement)node).GetAttribute("comboFrameBegin") );
				item.ComboTriggerFrameEnd_ = int.Parse( ((XmlElement)node).GetAttribute("comboFrameEnd") );
				
				ret = item;
				
				break;
			}
		}
		
		return ret;
	}
}
