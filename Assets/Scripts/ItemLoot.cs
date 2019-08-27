using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLoot : MonoBehaviour {

    #region Variaveis
    [System.Serializable]
    public class DropItem
    {
        public string name;
        public Item item;
        public int probability;
    }

    [Header("Array de itens existentes neste Loot Point")]
    public DropItem[] itemsToSearch;
    [Header("A probabilidade de conseguir itens deste Loot Point")]
    public int dropChance;

    [Header("Variaveis de Texto")]
    public Animator animFeedback;
    public Text textFeedback;

    [Header("Variáveis do Loot de item")]
    public Image interactImg;
    public float contAmount;
    public float timeToCatch;
    RectTransform handPos;

    [Header("Cooldown de Loot de Item")]
    [SerializeField] Light light;
    [SerializeField] Color color;
    float lightRange;
    float lightIntensity;
    [SerializeField] float contCooldown;
    [SerializeField] float contTimeCooldown;
    [SerializeField] bool startCooldown;

    bool canPress;

    Inventory inventory;
#endregion

    private void Start()
    {
        inventory = Inventory.inventory;
        light = GetComponent<Light>();
        color = light.color;
        lightRange = light.range;
        lightIntensity = light.intensity;
    }

    /// <summary>
    /// Função que calcula o loot e adiciona o item ao inventário do jogador.
    /// </summary>
    void calculateLoot()
    {
        int calc_dropChance = Random.Range(0, 101);
        // Se não conseguir nenhum loot.
        if (calc_dropChance > dropChance)
        {
            Debug.Log("Nenhum item encontrado");
            //Muda o texto de Feedback
            textFeedback.text = "Nenhum item encontrado";
            //Anima o texto de Feedback
            animFeedback.SetTrigger("Fade");
            return;
        }

        // Se conseguir algum loot.
        if(calc_dropChance <= dropChance)
        {
            // Variavel que armazena o valor de probabilidade total dos itens existentes no loot point.
            int itemChance = 0;

            // Varredura que soma os valores de probabilidade de cada item.
            for(int i = 0; i < itemsToSearch.Length; i++)
            {
                itemChance += itemsToSearch[i].probability;
            }
            //Debug.Log("itemChance = " + itemChance);

            // Valor aleatório dentro da probabilidade total dos itens.
            int randomValue = Random.Range(0, itemChance);
            //Debug.Log("randomValue = " + randomValue);

            // Varredura que verifica se o valor aleatório é menor que a probabilidade de algum dos itens do loot point.
            for(int j = 0; j < itemsToSearch.Length; j++)
            {
                // Se o valor aleatorio sorteado for menor que a probabilidade do item, este é adicionado ao inventário. O return seria para não conseguir mais de um item.
                if(randomValue <= itemsToSearch[j].probability)
                {
                    // Adicionar item no inventário.
                    inventory.AddItem(itemsToSearch[j].item);

                    //Muda o texto de Feedback
                    textFeedback.text = "+1 " + itemsToSearch[j].item.name;
                    //Anima o texto de Feedback
                    animFeedback.SetTrigger("Fade");

                    Debug.Log("Conseguiu o item: " + itemsToSearch[j].name);
                    return;
                }
                randomValue -= itemsToSearch[j].probability;
                //Debug.Log("randomValue reduzido: " + randomValue);
            }
        }
    }

    private void Update()
    {
        if (!this.startCooldown)
        {
            //muda a cor do Lootpoint para amarelo
            color = Color.yellow;
            ChangeLightProperties();

            if (canPress)
            {
                /* Se estiver sem cooldown
                 * E se puder interagir
                 * Pode pegar o Item
                 * */
                LoadingCathItem();
            }
        }
        /* Se cooldown está contando
         * Loot point não pode ser acessado
         */
        if (this.startCooldown)
        {
            //muda a cor do Lootpoint para vermelho
            color = Color.red;
            ChangeLightProperties();

            CountingCooldown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jogador entrou na área.");
            canPress = true;
            //ativa o gameObject de interacao
            interactImg.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jogador saiu da área.");
            canPress = false;
            interactImg.enabled = false;
        }
    }

    /// <summary>
    /// Pega Item e carrega Imagem de Interacao
    /// </summary>
    void LoadingCathItem()
    {
        
        //esta linha nao esta pegando a referencia do rectTransform
        //handPos = this.transform as RectTransform;
        //interactImg.GetComponent<RectTransform>().anchoredPosition = handPos.anchoredPosition;
        

        interactImg.fillAmount = this.contAmount / this.timeToCatch;

        //enquanto aperta o botão soma a barra circular
        if (Input.GetKey(KeyCode.E))
        {
            this.contAmount += Time.deltaTime;
            if (this.contAmount >= this.timeToCatch)
            {
                Debug.Log("Segurando o botão");
                calculateLoot();
                this.contAmount = 0f;

                //altera booleano, para iniciar o Cooldown do Loot point
                startCooldown = true;
            }
        }
    }

    /// <summary>
    /// Metodo para contar o cooldown do Loot point
    /// </summary>
    void CountingCooldown()
    {
        //Soma o tempo no Contador de Cooldown
        contCooldown += Time.deltaTime;
        if (contCooldown >= contTimeCooldown)
        {
            //recomeca contador de Cooldown
            contCooldown = 0f;
            //acaba o Cooldown
            startCooldown = false;
        }
    }

    /// <summary>
    /// Retorna para a cor da luz
    /// </summary>
    void ChangeLightProperties()
    {
        light.color = color;
    }
}
