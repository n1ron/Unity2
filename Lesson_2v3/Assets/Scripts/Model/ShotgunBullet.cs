namespace Geekbrains
{
	public sealed class ShotgunBullet : Ammunition
	{
		private void OnCollisionEnter(UnityEngine.Collision collision)
		{
			// дописать доп урон
			var tempObj = collision.gameObject.GetComponent<ISetDamage>();

			if (tempObj != null)
			{
				tempObj.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
			}

            Main.Instance.ObjectPooler.ResetObjectFromPool(this);
		}
	}
}