using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public int danno = 1;
    public float velocita = 3.0f;
    public float distanzaAttacco = 1.5f;
    public string playerTag = "Player";
    
    private Transform player;
    private Vector3 ultimaPosizione;
    private SpriteRenderer spriteRenderer; // Per flipare lo sprite invece di ruotare
    
    void Start()
    {
        // Cerca il player all'inizio
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
            ultimaPosizione = player.position;
        }
        else
        {
            Debug.LogError("Nessun oggetto con tag '" + playerTag + "' trovato nella scena!");
        }
        
        // Ottieni il componente SpriteRenderer (se presente)
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        // Se non abbiamo il player, prova a cercarlo di nuovo
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            return;
        }
        
        // Aggiorna l'ultima posizione conosciuta del player
        ultimaPosizione = player.position;
        
        // Calcola la distanza dal player
        float distanzaPlayer = Vector3.Distance(transform.position, ultimaPosizione);
        
        // Se il player è nel raggio di inseguimento
        if (distanzaPlayer < distanzaAttacco * 5)
        {
            // MOVIMENTO DIRETTO: Muovi il nemico verso il player
            Vector3 direzione = (ultimaPosizione - transform.position).normalized;
            
            // Usa Transform.Translate per un movimento diretto
            transform.Translate(direzione * velocita * Time.deltaTime, Space.World);
            
            // Invece di ruotare, possiamo flippare lo sprite in base alla direzione
            if (spriteRenderer != null)
            {
                // Se il nemico si muove verso sinistra, flippalo
                if (direzione.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                // Se il nemico si muove verso destra, ripristina l'orientamento originale
                else if (direzione.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
            
            // NON RUOTIAMO PIÙ IL NEMICO
            // La riga di rotazione è stata rimossa
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            PlayerHealt playerHealth = collision.gameObject.GetComponent<PlayerHealt>();
            if (playerHealth != null)
            {
                // Infliggi danno
                playerHealth.CheangeHealt(-danno);
                
                // Piccolo rimbalzo per evitare attacchi ripetuti
                Vector2 direzioneRimbalzo = (transform.position - collision.transform.position).normalized;
                transform.position += new Vector3(direzioneRimbalzo.x, direzioneRimbalzo.y, 0) * 0.5f;
            }
        }
    }
    
    // Debug visivo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanzaAttacco * 5);
        
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}