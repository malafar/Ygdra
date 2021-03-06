using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour {

    private ABB _abb;
    public UnityEngine.Object abbData;
    private SpriteRenderer _currentImg;
    private Text _currentText;
    private int _indexText;
    private Nodo _currentNodo;
    private List<KeyCode> _teclasValidas;

    void Start() {
        _abb = new ABB(AssetDatabase.GetAssetPath(abbData));
        _currentNodo = _abb.getRaiz();
        _currentImg = GameObject.FindGameObjectWithTag("Ilustracion").GetComponent<SpriteRenderer>();
        _currentImg.sprite = _currentNodo.getImg(); 
        _currentText = GameObject.FindGameObjectWithTag("Texto").GetComponent<Text>();
        _currentText.text = _currentNodo.getTexto()[0];
        _indexText = 0;
        _teclasValidas = new List<KeyCode>();
        _teclasValidas.Add(KeyCode.Space); // Simulador de pulsación
        _teclasValidas.Add(KeyCode.LeftArrow); // Simulador de decisión izq
        _teclasValidas.Add(KeyCode.RightArrow); // Simulador de decisión dcha
    }

    void Update() {
        if (Input.GetKeyUp(_teclasValidas[0]) || Input.GetKeyUp(_teclasValidas[1]) 
            || Input.GetKeyUp(_teclasValidas[2])) {
            next();
        }
    }

    private void next() {
        if (Input.GetKeyUp(_teclasValidas[0])) {
            if (_currentNodo.ultimoTexto(_indexText) && !_currentNodo.dosHijos()) {
                toNextNodo(_currentNodo.getHi());
            } else {
                _currentText.text = _currentNodo.nextText(_indexText);
                _indexText++;
            }
        } else {
            if (_currentNodo.ultimoTexto(_indexText) && _currentNodo.dosHijos()) {
                if (Input.GetKeyUp(_teclasValidas[1])) {
                    toNextNodo(_currentNodo.getHi());
                } else {
                    toNextNodo(_currentNodo.getHd());
                }
            }
        }
    }

    private void toNextNodo(Nodo next) {
        _currentNodo = next;
        _currentImg.sprite = _currentNodo.getImg();
        _indexText = 0;
        _currentText.text = _currentNodo.getTexto()[_indexText];
    }
}
