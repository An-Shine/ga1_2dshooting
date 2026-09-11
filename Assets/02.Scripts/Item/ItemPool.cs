using Unity.VisualScripting;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private static ItemPool _instance;
    public static ItemPool Instance => _instance;

    [Header("아이템 프리펩")]
    [SerializeField] private Item[] _itemPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _itemPoolSize;

    private Item[] _itemPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _itemPool = new Item[_itemPrefabs.Length * _itemPoolSize];

        int poolIndex = 0;

        foreach (Item itemPrefab in _itemPrefabs)
        {
            for (int i = 0; i < _itemPoolSize; i++)
            {
                Item item = Instantiate(itemPrefab, transform);
                item.gameObject.SetActive(false);
                _itemPool[poolIndex] = item;

                poolIndex++;
            }
        }
    }

    public Item GetItem(ItemType type, Vector3 position)
    {
        foreach (Item item in _itemPool)
        {
            if (item.Type != type)
            {
                continue;
            }

            if (item.gameObject.activeSelf)
            {
                continue;
            }

            item.transform.position = position;
            item.gameObject.SetActive(true);

            return item;
        }

        return null;
    }
}