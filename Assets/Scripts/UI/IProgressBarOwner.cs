using System;
using UnityEngine;

public interface IProgressBarOwner
{
	public class OnProgressChagedEventArgs : EventArgs
	{
		public float ProgressNormalized;
	}
	public event EventHandler<OnProgressChagedEventArgs> OnProgressChanged;
}
