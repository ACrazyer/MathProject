using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UserUI:MonoBehaviour
    {
        private InputField m00;
        private InputField m01;
        private InputField m10;
        private InputField m11;
        public MatrixPlane matrixPlane;
        private void Awake()
        {
            m00 = GameObject.Find("Matrix/m00").GetComponent<InputField>();
            m01 = GameObject.Find("Matrix/m01").GetComponent<InputField>();
            m10 = GameObject.Find("Matrix/m10").GetComponent<InputField>();
            m11 = GameObject.Find("Matrix/m11").GetComponent<InputField>();
            GameObject.Find("Convert").GetComponent<Button>().onClick.AddListener(onClickConvert);
        }

        private void onClickConvert()
        {
            Matrix2x2 matrix = new Matrix2x2(
                new Vector2(float.Parse(m00.text), float.Parse(m10.text)),
                new Vector2(float.Parse(m01.text), float.Parse(m11.text)));
            matrixPlane.LerpConvert(matrix);
        }
    }
}
