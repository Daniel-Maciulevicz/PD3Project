using System.Collections.Generic;

public abstract class Pool
{
    private int _itemToActivate = 0;

    public Dictionary<int, IPoolItem> Items { get; set; }

    public virtual void ActivateNextItem()
    {
        Items[_itemToActivate].Reset();

        Items[_itemToActivate].Activate();

        _itemToActivate++;

        if (_itemToActivate >= Items.Count)
            _itemToActivate = 0;
    }
    public virtual void DeactivateItem(IPoolItem item)
    {
        item.Deactivate();
    }
    public abstract void GeneratePool(int size);

    public Pool(int size)
    {
        Items = new Dictionary<int, IPoolItem>();
    }
}