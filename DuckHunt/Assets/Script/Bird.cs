using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	public float velocidade;
	public float velocidadeLateral = 1.5f;
	public float velocidadeVertical = 1.5f;
	Vector2 posicaoFinal;
	float posX;
	float posY;
	Animator anim;
	SpriteRenderer sr;
	bool esquerda;
	bool direita;
	bool cima;
	bool baixo;
	bool chegou;

	// Use this for initialization
	void Start () {
		posX = Mathf.Round (Random.Range (-6, 6));
		posY = Mathf.Round (Random.Range (1, 4));
		posicaoFinal = new Vector2 (posX,posY);
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
	




	}
	
	// Update is called once per frame
	void Update () {

			if (!chegou) {
				transform.position = Vector2.MoveTowards (transform.position, posicaoFinal, velocidade * Time.deltaTime);
			}

			if (transform.position.x == posX && transform.position.y == posY) {
			
				if (transform.position.x < 0) {
					direita = true;
					sr.flipX = false;
				} else if (transform.position.x > 0) {
					esquerda = true;
					sr.flipX = true;

				} else {
					int direcao = Random.Range (0, 1);
					if (direcao == 1) {
						direita = true;
					} else if (direcao == 0) {
						esquerda = true;
					}

				}
				if (transform.position.y < 2) {
					cima = true;
				} else if (transform.position.y > 2) {
					baixo = true;
				} else {
					int direcao2 = Random.Range (0, 1);
					if (direcao2 == 1) {
						cima = true;
					} else if (direcao2 == 0) {
						baixo = true;
					}
				
				}
				chegou = true;


		
			}
			if (esquerda) {
				transform.Translate (Vector2.left * velocidadeLateral * Time.deltaTime);
			}
			if (direita) {
				transform.Translate (Vector2.right * velocidadeLateral * Time.deltaTime);
			}
			if (cima) {
				transform.Translate (Vector2.up * velocidadeVertical * Time.deltaTime);
			}
			if (baixo) {
				transform.Translate (Vector2.down * velocidadeVertical * Time.deltaTime);
			}
			if (chegou) {
				anim.SetBool ("Idle", true);
				//setando limites de voo apos chegada no ponto
				if (transform.position.x <= -6.3) {
					velocidadeLateral *= -1;
					sr.flipX = false;
				} else if (transform.position.x >= 6.3) {
					velocidadeLateral *= -1;
					sr.flipX = true;
				}
				if (transform.position.y <= -1.5) {
					velocidadeVertical *= -1;
				} else if (transform.position.y >= 4.5) {
					velocidadeVertical *= -1;
				}



		}
	
		
	}
	IEnumerator Esquerda(){
		transform.Translate (Vector2.left * velocidadeLateral * Time.deltaTime);
		chegou = false;
		sr.flipX = false;
		yield return new WaitForSeconds (1.0f);
		//StartCoroutine (Direita());
	}
	IEnumerator Direita(){
		transform.Translate (Vector2.right * velocidadeLateral * Time.deltaTime);

		sr.flipX = true;
		yield return new WaitForSeconds (1.0f);
		StartCoroutine (Esquerda());
	}

}





