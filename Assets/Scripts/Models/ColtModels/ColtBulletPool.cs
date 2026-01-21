using PD3Stars.Models;
using System;

public class ColtBulletPool : Pool
{
    public event EventHandler<ColtBulletCreatedArgs> ColtBulletCreated;

    public override void GeneratePool(int size)
    {
        for (int b = 0; b < size; b++)
        {
            ColtBullet bullet = new ColtBullet();
            Items.Add(b, bullet);
            ColtBulletCreated(this, new ColtBulletCreatedArgs(bullet));
            bullet.Deactivate();
        }
    }

    public ColtBulletPool() : base(20) { }
}