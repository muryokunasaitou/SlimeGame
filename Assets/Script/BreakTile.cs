using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakTile : MonoBehaviour
{
    //なんかすきなParticleを作ってエディタでアタッチしましょう
    [SerializeField] private ParticleSystem particle;


    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D ot)
    {
        if (ot.gameObject.name == "Tilemap_B_Break_Ground")
        {
            var hitPos = Vector3.zero;
            //あたった場所の座標を取得
            foreach (var point in ot.contacts)
            {
                hitPos = point.point;
            }

            var position = ot.gameObject.GetComponent<Tilemap>().cellBounds.allPositionsWithin;
            //一番近い場所を保存したいので変数を宣言
            var minPosition = 0;
            var allPosition = new List<Vector3>();

            foreach (var variable in position)
            {
                if (ot.gameObject.GetComponent<Tilemap>().GetTile(variable) != null)
                {
                    allPosition.Add(variable);
                }
            }

            //for文で探査する。でも初期化で0入れてるから1からスタート
            for (var i = 1; i < allPosition.Count; i++)
            {
                //それぞれのあたった場所からの大きさを取得、最小を更新したらminPositionNumを更新する
                if ((hitPos - allPosition[i]).magnitude <
                    (hitPos - allPosition[minPosition]).magnitude)
                {
                    minPosition = i;
                }
            }

            //最終的な位置を一旦格納した。RoundToIntは四捨五入とのことです
            var finalPosition = Vector3Int.RoundToInt(allPosition[minPosition]);

            var tiletmp = ot.gameObject.GetComponent<Tilemap>().GetTile(finalPosition);

            if (tiletmp != null)
            {
                var map = ot.gameObject.GetComponent<Tilemap>();
                var tileCol = ot.gameObject.GetComponent<TilemapCollider2D>();
                map.SetTile(finalPosition, null);
                tileCol.enabled = false;
                tileCol.enabled = true;

                //わたしは1秒寿命でサイズを変えて、0.85fで消したらいい感じになったのでこんな感じにしました
                Destroy(Instantiate(particle, finalPosition + Vector3.one * 0.5f, Quaternion.identity), 0.85f);
            }
        }
    }
}
