using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingBenchSave : MonoBehaviour
{
    [System.Serializable]

    public class CraftingItem
    {
        public string itemName;

        public int amountProduced;
        public float craftTime;
        public List<string> itemsConsumed;
        public List<int> amountConsumed;

        public int stackAmount;

        public CraftingItem(string iN, int aP, float cT, List<string> iC, List<int> aC, int sA)
        {
            itemName = iN;
            amountProduced = aP;
            craftTime = cT;
            itemsConsumed = iC;
            amountConsumed = aC;
            stackAmount = sA;
        }
    }
    [System.Serializable]

    public class CraftingSave
    {
        public int craftingBenchId;

        public string player;

        public int levelWorkBench;
        public CraftingItem craftingItem;
        public bool craft;
        public int whatSlot;
        public int allSlots;
        public bool deliever;
        public bool craftingItemBoolean;
        public List<int> amountCheck;
        public int amountProduced;
        public bool craftButtonBool;
        public List<string> materialsNeeded;
        public List<int> materialAmountNeed;
        public int materialCheckAmount;
        public int devlierMaterialCheckAmount;

        public float craftingTime;

        public int overLimit;


        public List<CraftingItem> queue;
        public CraftingSave(int cBI, string ply, int lwb, CraftingItem cI, bool c, int ws, int aS, bool d, bool cIB, List<int> aC, int ap, bool cbb, List<string> mN, List<int> mAN, int mCA, int dMCA, float cT, int oL, List<CraftingItem> lCI)
        {
            craftingBenchId = cBI;
            player = ply;

            levelWorkBench = lwb;
            craftingItem = cI;
            craft = c;
            whatSlot = ws;
            allSlots = aS;
            deliever = d;
            craftingItemBoolean = cIB;
            amountCheck = aC;
            amountProduced = ap;
            craftButtonBool = cbb;
            materialsNeeded = mN;
            materialAmountNeed = mAN;
            materialCheckAmount = mCA;
            devlierMaterialCheckAmount = dMCA;

            craftingTime = cT;

            overLimit = oL;
            queue = lCI;
        }
    }
    [SerializeField]
    public craftingBenchSaveData benchSave;
    [System.Serializable]

    public class craftingBenchSaveData
    {
        public List<CraftingSave> craftingSave;
        public craftingBenchSaveData(List<CraftingSave> cS)
        {
            craftingSave = cS;
        }
    }

    public List<craftingBench> craftingBenches;
    public SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        if (!saveManager)
        {
            saveManager = GameObject.FindObjectOfType<SaveManager>();
        }
    }




    public IEnumerator load(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var bench in craftingBenches)
        {
            foreach (var save in benchSave.craftingSave)
            {
                if (bench.craftingBenchId == save.craftingBenchId)
                {
                    bench.player = GameObject.Find( save.player);

                    bench.levelWorkBench = save.levelWorkBench;
                    bench.craft = save.craft;
                    bench.craftingItem = save.craftingItemBoolean;
                    bench.whatSlot = save.whatSlot;
                    bench.allSlots = save.allSlots;
                    bench.deliever = save.deliever;
                    bench.item = new craftingBench.Item(save.craftingItem.itemName, save.craftingItem.amountProduced, save.craftingItem.craftTime, save.craftingItem.itemsConsumed, save.craftingItem.amountConsumed, save.craftingItem.stackAmount);
                    bench.amountCheck = save.amountCheck;
                    bench.amountProduced = save.amountProduced;
                    if(bench.item.itemName != null && bench.item.itemName != "")
                        {
                        bench.craftButtonBool = true;

                    }
                    bench.materialsNeeded = save.materialsNeeded;
                    bench.materialAmountNeed = save.materialAmountNeed;
                    bench.materialCheckAmount = save.materialCheckAmount;

                    bench.devlierMaterialCheckAmount = save.devlierMaterialCheckAmount;

                    bench.craftingTime = save.craftingTime;

                    bench.overLimit = save.overLimit;

                    List<craftingBench.Item> q = new List<craftingBench.Item>();
                    for (int x = 0; x < save.queue.Count; x++)
                    {
                        if (!q.Contains(new craftingBench.Item(save.queue[x].itemName, save.queue[x].amountProduced, save.queue[x].craftTime, save.queue[x].itemsConsumed, save.queue[x].amountConsumed, save.queue[x].stackAmount)))
                        {
                            q.Add(new craftingBench.Item(save.queue[x].itemName, save.queue[x].amountProduced, save.queue[x].craftTime, save.queue[x].itemsConsumed, save.queue[x].amountConsumed, save.queue[x].stackAmount));
                        }
                    }
                    bench.queue = q;
                }
            }
        }
    }

    public void updateInformation()
    {
      
            
                benchSave.craftingSave =  new List<CraftingSave>();
            
        
        for (int i = 0; i < craftingBenches.Count; i++)
        {
            List<CraftingItem> q = new List<CraftingItem>();
            for (int x = 0; x < craftingBenches[i].queue.Count; x++)
            {


                if (!q.Contains(new CraftingItem(craftingBenches[i].queue[x].itemName, craftingBenches[i].queue[x].amountProduced, craftingBenches[i].queue[x].craftTime, craftingBenches[i].queue[x].itemsConsumed, craftingBenches[i].queue[x].amountConsumed, craftingBenches[i].queue[x].stackAmount)))
                {
                    q.Add(new CraftingItem(craftingBenches[i].queue[x].itemName, craftingBenches[i].queue[x].amountProduced, craftingBenches[i].queue[x].craftTime, craftingBenches[i].queue[x].itemsConsumed, craftingBenches[i].queue[x].amountConsumed, craftingBenches[i].queue[x].stackAmount));
                }



            }
            if (craftingBenches[i].player)
            {
                if (!benchSave.craftingSave.Contains(new CraftingSave(craftingBenches[i].craftingBenchId, craftingBenches[i].player.name, craftingBenches[i].levelWorkBench, new CraftingItem(craftingBenches[i].item.itemName, craftingBenches[i].item.amountProduced, craftingBenches[i].item.craftTime, craftingBenches[i].item.itemsConsumed, craftingBenches[i].item.amountConsumed, craftingBenches[i].item.stackAmount), craftingBenches[i].craft, craftingBenches[i].whatSlot, craftingBenches[i].allSlots, craftingBenches[i].deliever, craftingBenches[i].craftingItem, craftingBenches[i].amountCheck, craftingBenches[i].amountProduced, craftingBenches[i].craftButtonBool, craftingBenches[i].materialsNeeded, craftingBenches[i].materialAmountNeed, craftingBenches[i].materialCheckAmount, craftingBenches[i].devlierMaterialCheckAmount, craftingBenches[i].craftingTime, craftingBenches[i].overLimit, q)))
                {
                    benchSave.craftingSave.Add(new CraftingSave(craftingBenches[i].craftingBenchId, craftingBenches[i].player.name, craftingBenches[i].levelWorkBench, new CraftingItem(craftingBenches[i].item.itemName, craftingBenches[i].item.amountProduced, craftingBenches[i].item.craftTime, craftingBenches[i].item.itemsConsumed, craftingBenches[i].item.amountConsumed, craftingBenches[i].item.stackAmount), craftingBenches[i].craft, craftingBenches[i].whatSlot, craftingBenches[i].allSlots, craftingBenches[i].deliever, craftingBenches[i].craftingItem, craftingBenches[i].amountCheck, craftingBenches[i].amountProduced, craftingBenches[i].craftButtonBool, craftingBenches[i].materialsNeeded, craftingBenches[i].materialAmountNeed, craftingBenches[i].materialCheckAmount, craftingBenches[i].devlierMaterialCheckAmount, craftingBenches[i].craftingTime, craftingBenches[i].overLimit, q));
                }
            }
            else
            {
                if (!benchSave.craftingSave.Contains(new CraftingSave(craftingBenches[i].craftingBenchId, null, craftingBenches[i].levelWorkBench, new CraftingItem(craftingBenches[i].item.itemName, craftingBenches[i].item.amountProduced, craftingBenches[i].item.craftTime, craftingBenches[i].item.itemsConsumed, craftingBenches[i].item.amountConsumed, craftingBenches[i].item.stackAmount), craftingBenches[i].craft, craftingBenches[i].whatSlot, craftingBenches[i].allSlots, craftingBenches[i].deliever, craftingBenches[i].craftingItem, craftingBenches[i].amountCheck, craftingBenches[i].amountProduced, craftingBenches[i].craftButtonBool, craftingBenches[i].materialsNeeded, craftingBenches[i].materialAmountNeed, craftingBenches[i].materialCheckAmount, craftingBenches[i].devlierMaterialCheckAmount, craftingBenches[i].craftingTime, craftingBenches[i].overLimit, q)))
                {
                    benchSave.craftingSave.Add(new CraftingSave(craftingBenches[i].craftingBenchId, null, craftingBenches[i].levelWorkBench, new CraftingItem(craftingBenches[i].item.itemName, craftingBenches[i].item.amountProduced, craftingBenches[i].item.craftTime, craftingBenches[i].item.itemsConsumed, craftingBenches[i].item.amountConsumed, craftingBenches[i].item.stackAmount), craftingBenches[i].craft, craftingBenches[i].whatSlot, craftingBenches[i].allSlots, craftingBenches[i].deliever, craftingBenches[i].craftingItem, craftingBenches[i].amountCheck, craftingBenches[i].amountProduced, craftingBenches[i].craftButtonBool, craftingBenches[i].materialsNeeded, craftingBenches[i].materialAmountNeed, craftingBenches[i].materialCheckAmount, craftingBenches[i].devlierMaterialCheckAmount, craftingBenches[i].craftingTime, craftingBenches[i].overLimit, q));
                }
            }
        }
        saveManager.UpdateCraftingBenchInformation();
    }
}
