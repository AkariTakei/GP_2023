using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class MagnetMenu : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler

{
    // 吸いつく位置の中心座標
    [SerializeField] private Vector2 magnetPosition;
    // 等間隔に並べる要素と要素の間隔
    [SerializeField] private float itemDistance;
    // 制御対象の子要素
    [SerializeField] List<RectTransform> items;
    // 初期表示したときに中央に表示する値
    [SerializeField] int centerElemIndex;

    [SerializeField] private GameObject selectItem;

    // アニメーション中かどうかのフラグ
    // true : 実行中 / false : それ以外
    [SerializeField] private bool isDragging;

    private void Awake()
    {
        this.updateItemsScale();
    }

    private void OnDestroy()
    {
        this.tweenList.KillAllAndClear();
    }

    private void Update()
    {
        if (!this.isDragging)
        {
            return;
        }
        this.updateItemsScale();
    }

    private void updateItemsScale()
    {
        foreach (var item in this.items)
        {
            float distance = Mathf.Abs(item.GetAnchoredPosX());
            float scale = Mathf.Clamp(1.0f - distance / (170.0f * 4.0f), 0.65f, 1.0f);
            item.SetLocalScaleXY(scale);
        }
    }

    public void OnDrag(PointerEventData e)
    {
        // 操作量に応じてX方向に移動する
        float delta_x = e.delta.x;

        if (items[0].GetAnchoredPosX() < -500f)
        {
            // items[0] の RectTransform を一時保管
            RectTransform tempRect = items[0];
            // リストから items[0] を削除
            items.RemoveAt(0);
            // items[5] と 30 の間隔を開けて、一時保管しておいた RectTransform を移動
            tempRect.anchoredPosition = new Vector2(items[5].GetAnchoredPosX() + itemDistance, tempRect.anchoredPosition.y);
            // 一時保管しておいた RectTransform を新たにリストに追加
            items.Add(tempRect);
        }

        if (items[6].GetAnchoredPosX() > 500f)
        {
            // items[6] の RectTransform を一時保管
            RectTransform tempRect = items[6];
            // リストから items[6] を削除
            items.RemoveAt(6);
            // items[0] と間隔を開けて、一時保管しておいた RectTransform を移動
            tempRect.anchoredPosition = new Vector2(items[0].GetAnchoredPosX() - itemDistance, tempRect.anchoredPosition.y);
            // 一時保管しておいた RectTransform をitems[0]の前にリストに追加
            items.Insert(0, tempRect);
        }


        foreach (var item in this.items)
        {
            RectTransform rect = item;
            var pos = rect.anchoredPosition;
            pos.x += delta_x;
            rect.anchoredPosition = pos;
        }

    }

    public void OnBeginDrag(PointerEventData e)
    {
        this.isDragging = true;
        this.tweenList.KillAllAndClear();
    }

    public void OnEndDrag(PointerEventData e)
    {
        // 移動目標量を計算
        RectTransform rect = this.pickupNearestRect();
        float tartgetX = -rect.GetAnchoredPosX();

        for (int i = 0; i < this.items.Count; i++)
        {
            RectTransform item = this.items[i];

            Tween t =
                item.DOAnchorPosX(item.GetAnchoredPosX()
                    + tartgetX, 0.075f).SetEase(Ease.OutSine);
            if (i <= this.items.Count)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(t);
                seq.AppendCallback(this.onCompleted);
                this.tweenList.Add(seq);
            }
            else
            {
                this.tweenList.Add(t);
            }
        }
    }

    private void onCompleted() => this.isDragging = false;
    private List<Tween> tweenList = new List<Tween>();

    // マグネット中心に最も近い要素を選択する
    private RectTransform pickupNearestRect()
    {
        RectTransform nearestRect = null;
        foreach (var rect in this.items)
        {
            if (nearestRect == null)
            {
                nearestRect = rect; // 初回選択
            }
            else
            {
                if (Mathf.Abs(rect.GetAnchoredPosX())
                    < Mathf.Abs(nearestRect.GetAnchoredPosX()))
                {
                    nearestRect = rect; // より中心に近いほうを選択
                }
            }
        }
        selectItem = nearestRect.gameObject;
        return nearestRect;
    }

}

public static class List_Tween_Extension
{
    // リスト内のすべてのアニメーションを停止します
    public static void KillAllAndClear(this List<Tween> self)
    {
        self.ForEach(tween => tween.Kill());
        self.Clear();
    }
}

public static class RectTransformExtension
{
    // Xのアンカー位置を取得する
    public static float GetAnchoredPosX(this RectTransform self)
    {
        return self.anchoredPosition.x;
    }
    // オブジェクトの拡大率の設定
    public static void SetLocalScaleXY(this Transform self, float xy)
    {
        Vector3 scale = self.localScale;
        scale.x = xy;
        scale.y = xy;
        self.localScale = scale;
    }
}
