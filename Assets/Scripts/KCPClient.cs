using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class KCPClient : MonoBehaviour {
    int counter = 0;
    UDPSession conn = new UDPSession();
    byte[] buff = new byte[1024];
    void Start() {
        conn.Connect("127.0.0.1", 6789);
    }

    private void Update() {
        conn.Update();
        var n = conn.Recv(buff, 0, 1024);
        if (n == 0) {
            // no data
        } else if (n < 0) {
            Debug.LogError("recv error");
        } else {
            var resp = Encoding.UTF8.GetString(buff, 0, n);
            Debug.Log("recv:" + resp);
        }
    }

    public void SendHello() {
        var text = Encoding.UTF8.GetBytes(string.Format("Hello KCP: {0}", ++counter));
        var rs = conn.Send(text, 0, text.Length);
        if (rs < 0) {
            Debug.LogError("send error");
        }
    }
}
