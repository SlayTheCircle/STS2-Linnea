# 图标保持比例,居中补透明底到 256²;遗物/药水供齐独立描边槽。
make_icons() {
    local destination="$1" mapping_name="$2" outline="$3"
    local -n mapping="$mapping_name"
    local path cls
    mkdir -p "$DST/$destination"
    for path in "${!mapping[@]}"; do
        cls="${mapping[$path]}"
        convert "$SRC/$path" -resize 256x256 -background none -gravity center -extent 256x256 "$DST/$destination/$cls.png"
        if [[ "$outline" == 1 ]]; then
            convert "$DST/$destination/$cls.png" -bordercolor none -border 6 -alpha extract -morphology Dilate Disk:5 -gravity center -crop 256x256+0+0 +repage "$TMP/mask.png"
            convert -size 256x256 xc:"$OUTLINE_COLOR" "$TMP/mask.png" -alpha off -compose CopyOpacity -composite "$DST/$destination/${cls}_outline.png"
        fi
    done
}
make_icons relics RELICS 1
make_icons potions POTIONS 1
make_icons powers POWERS 0
make_icons enchantments ENCHANTMENTS 0
if [[ -n "$SUPPORT_BADGE_SOURCE" ]]; then
    mkdir -p "$DST/enchantments"
    convert "$SRC/$SUPPORT_BADGE_SOURCE" -resize 256x256 -background none -gravity center -extent 256x256 "$DST/enchantments/${MOD_SHORT,,}_support.png"
fi
echo '图标与描边生成完成。'

# 培育按钮使用交付的专用选项母版；保持母版不变。
mkdir -p "$DST/ui"
convert "$SRC/图片/事件额外火堆选项：培育.png" -resize 256x256 -background none -gravity center -extent 256x256 "$DST/ui/CultivateRestSiteOption.png"
# 同一能力的等待阶段使用相同原稿图标。
cp "$DST/powers/SpokenOmenPower.png" "$DST/powers/OmenDamagePower.png"
