//using JigsawPuzzlesCollection.Scripts.Ads;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace JigsawPuzzlesCollection.Scripts
{
    [RequireComponent(typeof(Button))]
    public class CollectionScript : MonoBehaviour
    {
        private Collection m_collection;
       
        public void SetDetails(Collection collection)
        {
            m_collection = collection;
            
            var button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                LevelSelectorScript.Instance.SelectedCollection = collection;
                MenuScript.Instance.DisplayCollection(collection);
            });

            var cardContentElement = transform.Find("CardContent");
            
            var imageElement = cardContentElement.Find("Image");
            imageElement.GetComponent<Image>().sprite = collection.Image;

            var textElement = cardContentElement.Find("Text");
            textElement.GetComponent<Text>().text = collection.name;

            var highlightElement = imageElement.Find("Lock");
            highlightElement.gameObject.SetActive(false);

        }
    }
}