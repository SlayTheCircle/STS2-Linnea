# 世界线四图 → 四章纪元资源(设计:世界线剧情四章按序对应;Dehya J6 同型口径)。
# 全部按字节比较更新，避免母版换图后被存在性或 mtime 守卫跳过。
mkdir -p "$DST/timeline"

# 纪元大图使用原版推导的全局路径;缩略图走 RitsuLib 的 mod 资源槽。
DST_G="$MOD_ROOT/assets/global/images/timeline/epoch_portraits"
mkdir -p "$DST_G"
for n in 1 2 3 4; do
    # 母版 648×365,槽位 1672×941(事件肖像同款缩放,宽高比一致无有效裁切)。
    convert "$SRC/图片/世界线$n.png" -resize 1672x941^ -gravity center -extent 1672x941 "$DST_G/sts2_linnea_epoch_$n.png"
done
for n in 1 2 3 4; do
    convert "$DST_G/sts2_linnea_epoch_$n.png" -resize 272x174^ -gravity center -extent 272x174 \
        "$DST/timeline/sts2_linnea_epoch_${n}_thumb.png"
done
echo "纪元立绘: 四张世界线图 + 四张派生缩略图就绪"
