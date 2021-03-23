namespace Insepter.Ads
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Advertisements;

    public class BannerAds : MonoBehaviour
    {
        public string surfacingId = "bannerPlacement";
        public BannerPosition bannerPosition;
        IEnumerator Start()
        {
            while (!Advertisement.isInitialized)
            {
                yield return new WaitForSeconds(0.5f);
            }
            Advertisement.Banner.Show(surfacingId);
            Advertisement.Banner.SetPosition(bannerPosition);
        }
    }
}