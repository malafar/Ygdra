using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour {

    public Text estado_txt; // TODO: borrar cuando se termine con el simulador provisional
    private ABB _abb;
    private Image _currentImg;
    private Text _currentText;
    private int _indexText;
    private Nodo _currentNodo;
    private List<KeyCode> _teclasValidas;

    void Start() {
        _abb = new ABB("dataABBPruebas");
        // TODO: cambiar a esto cuando sea versión desde UI _abb = new ABB(GameManager.getNombreABB());
        _currentNodo = _abb.getRaiz();
        _currentImg = GameObject.FindGameObjectWithTag("Ilustracion").GetComponent<Image>();
        _currentImg.sprite = _currentNodo.getImg(); 
        _currentText = GameObject.FindGameObjectWithTag("Texto").GetComponent<Text>();
        _currentText.text = _currentNodo.getTexto()[0];
        _indexText = 0;
        _teclasValidas = new List<KeyCode>();
        _teclasValidas.Add(KeyCode.Space); // Simulador de pulsación
        _teclasValidas.Add(KeyCode.LeftArrow); // Simulador de decisión izq
        _teclasValidas.Add(KeyCode.RightArrow); // Simulador de decisión dcha
        estado_txt.text = _currentNodo.getState().ToString();
    }

    void Update() {
        if (Input.GetKeyUp(_teclasValidas[0]) || Input.GetKeyUp(_teclasValidas[1]) 
            || Input.GetKeyUp(_teclasValidas[2])) {
            next();
        }
        // TODO: quitar para cuando se vaya a dejar de simular en editor
    }

    public void nextFront() {
        if (Player.getCurrentHojas() > 0) {
            if (!_currentNodo.nodoFinal()) {
                if (!_currentNodo.ultimoTexto(_indexText)) {
                    _currentText.text = _currentNodo.nextText(_indexText);
                    _indexText++;
                } else if (_currentNodo.ultimoTexto(_indexText) && !_currentNodo.dosHijos()) {
                    toNextNodo(_currentNodo.getHi());
                }
            } else {
                updateStateNodo(_currentNodo);
            }
        }
    }

    public void nextLeft() {
        if (Player.getCurrentHojas() > 0) {
            if (!_currentNodo.nodoFinal()) {
                if (_currentNodo.ultimoTexto(_indexText) && _currentNodo.dosHijos()) {
                    toNextNodo(_currentNodo.getHi());
                }
            }
        }
    }

    public void nextRight() {
        if (Player.getCurrentHojas() > 0) {
            if (!_currentNodo.nodoFinal()) {
                if (_currentNodo.ultimoTexto(_indexText) && _currentNodo.dosHijos()) {
                    toNextNodo(_currentNodo.getHd());
                }
            }
        }
    }

    private void next() {
        // TODO: quitar para cuando se vaya a dejar de simular en editor
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
        estado_txt.text = _currentNodo.getState().ToString();

        GameManager.updateCntHojas(false);
    }

    private void updateStateNodo(Nodo nodo, Nodo next = null) {
        // TODO: en los nodos finales hay que darle de neuvo al next para que se actualice,
        // ya que ese pulsado extra simula el texto del FIN
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

    public ABB getABB() {
        return _abb;
    }
}
