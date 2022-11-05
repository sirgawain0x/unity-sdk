using UnityEngine;
using Thirdweb;
using TMPro;

public class SDKTest : MonoBehaviour
{
    private ThirdwebSDK sdk;
    private Contract contract;
    private int count;
    public TMP_Text loginButton;
    public TMP_Text fetchButton;
    public TMP_Text claimButton;

    void Start()
    {
        sdk = new ThirdwebSDK("goerli");
        contract = sdk.GetContract("0x2e01763fA0e15e07294D74B63cE4b526B321E389");
    }

    void Update() {
    }

    public async void OnLoginCLick()
    {
        Debug.Log("Login clicked");
        
        loginButton.text = "Connecting...";
        string address = await sdk.Connect();
        loginButton.text = "Connected as: " + address.Substring(0, 6) + "...";
    }

    public async void OnButtonClick()
    {
        Debug.Log("Button clicked");
        count++;
        fetchButton.text = "Fetching Token: " + count;
        NFT result = await contract.ERC721.Get(count.ToString());
        Debug.Log("name: " + result.metadata.name);
        Debug.Log("owner: " + result.owner);
        fetchButton.text = result.metadata.name;
        // int supply = await contract.ERC721.TotalClaimedSupply();
        // fetchButton.text = supply.ToString();
    }

    public async void OnWriteButtonClick()
    {
        Debug.Log("Claim button clicked");
        count++;
        //var result = await contract.ERC721.Transfer("0x2247d5d238d0f9d37184d8332aE0289d1aD9991b", count.ToString());
        claimButton.text = "claiming...";
        var result = await contract.ERC721.Claim(1);
        Debug.Log("result id: " + result[0].id);
        Debug.Log("result receipt: " + result[0].receipt.transactionHash);
        claimButton.text = "claimed tokenId: " + result[0].id;
    }
}
