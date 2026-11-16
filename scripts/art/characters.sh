# 按角色姿态统一逻辑像素密度；原件留在母版目录，成品按字节更新。
mkdir -p "$DST/characters"
for pose in "${!CHARACTER_POSES[@]}"; do
    convert "$SRC/${CHARACTER_POSES[$pose]}" -resize "x${CHARACTER_HEIGHTS[$pose]}" "$DST/characters/${MOD_SHORT,,}_$pose.png"
done
convert "$SRC/$CHARACTER_BACKGROUND" -resize 1672x941^ -gravity center -extent 1672x941 "$DST/characters/${MOD_SHORT,,}_char_select_bg.png"
convert "$DST/characters/${MOD_SHORT,,}_normal.png" -resize 248x374 -background none -gravity center -extent 264x390 "$DST/characters/${MOD_SHORT,,}_char_select.png"
convert "$DST/characters/${MOD_SHORT,,}_char_select.png" -fill '#171c29' -colorize 100 "$DST/characters/${MOD_SHORT,,}_char_select_locked.png"
convert "$SRC/$CHARACTER_AVATAR" -resize 80x80 -background none -gravity center -extent 88x88 "$DST/characters/${MOD_SHORT,,}_icon.png"
convert "$DST/characters/${MOD_SHORT,,}_icon.png" -alpha extract -morphology Dilate Disk:2 "$TMP/icon_mask.png"
convert -size 88x88 xc:"$OUTLINE_COLOR" "$TMP/icon_mask.png" -alpha off -compose CopyOpacity -composite "$DST/characters/${MOD_SHORT,,}_icon_outline.png"
convert "$SRC/$CHARACTER_AVATAR" -resize 60x60 -background none -gravity center -extent 64x64 "$DST/characters/${MOD_SHORT,,}_map_marker.png"
echo '角色姿态、选人、头像与地图标记生成完成。'
