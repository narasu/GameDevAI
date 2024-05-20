using UnityEngine;

public interface IDetectable
{
	public void CallDetected(Transform _agent);
	public void CallEscaped();
}