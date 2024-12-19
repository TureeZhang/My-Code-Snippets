function AdjustImageSize() {
    var targetWidthCm = 14.7; // 目标宽度，单位：厘米
    var targetWidth = targetWidthCm * 28.35; // 转换为点（WPS 使用点为单位）

    var shapes = Application.ActiveDocument.InlineShapes; // 获取所有内嵌图片
    for (var i = 1; i <= shapes.Count; i++) { 
        var pic = shapes.Item(i); // 索引从1开始
        pic.LockAspectRatio = true; // 保持纵横比
        pic.Width = targetWidth; // 设置宽度，高度自动按比例调整
    }

    alert("图片大小已调整为：\n宽度: " + targetWidthCm + " 厘米\n高度自动保持比例。");
}
