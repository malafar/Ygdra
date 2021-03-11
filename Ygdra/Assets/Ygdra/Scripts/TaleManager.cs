using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour {

    public ABB _abb;
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
        if (!_currentNodo.nodoFinal()) {
            if (Input.GetKeyUp(_teclasValidas[0])) {
                if (!_currentNodo.ultimoTexto(_indexText)) {
                    _currentText.text = _currentNodo.nextText(_indexText);
                    _indexText++;
                } else if (_currentNodo.ultimoTexto(_indexText) && !_currentNodo.dosHijos()) {
                    toNextNodo(_currentNodo.getHi());
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
        } else {
            if (Input.GetKeyUp(_teclasValidas[0])) {
                updateStateNodo(_currentNodo);
            }
        }
    }

    private void toNextNodo(Nodo next) {
        updateStateNodo(_currentNodo, next);
        _currentNodo = next;
        _currentImg.sprite = _currentNodo.getImg();
        _indexText = 0;
        _currentText.text = _currentNodo.getTexto()[_indexText];
    }

    private void updateStateNodo(Nodo nodo, Nodo next = null) {
        if (!nodo.dosHijos() && nodo.getState() != State.VISITADO) {
            nodo.setState(State.VISITADO);
        } else if (nodo.dosHijos() && nodo.getState() != State.VISITADO) {
            if (nodo.getState() == State.NO_VISITADO) {
                nodo.setState(State.PARCIAL);
            } else {
                if(((nodo.getHi().getState() == State.PARCIAL || nodo.getHi().getState() == State.VISITADO)
                    && next == nodo.getHd()) || ((nodo.getHd().getState() == State.PARCIAL 
                    || nodo.getHd().getState() == State.VISITADO) && next == nodo.getHi())) {
                    nodo.setState(State.VISITADO);
                }
            }
        }
    }
}
