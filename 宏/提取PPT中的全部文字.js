function extractTextFromPPT() {
    var pptApp = Application;  // 获取WPS应用对象
    var presentation = pptApp.ActivePresentation;  // 获取当前活动PPT
    var extractedText = "";  // 初始化存储提取文本的字符串

    // 遍历每一张幻灯片
    for (var i = 1; i <= presentation.Slides.Count; i++) {
        var slide = presentation.Slides(i);

        // 遍历每个形状
        for (var j = 1; j <= slide.Shapes.Count; j++) {
            var shape = slide.Shapes(j);

            // 判断形状是否有文本框
            if (shape.HasTextFrame && shape.TextFrame.HasText) {
                // 提取文本
                extractedText += "Slide " + i + ": \n" + shape.TextFrame.TextRange.Text + "\n\n";
            }
        }
    }

    // 如果提取到文本，则显示文本内容
    if (extractedText !== "") {
        WPS.Application.MessageBox("提取的文本内容：\n\n" + extractedText);
    } else {
        WPS.Application.MessageBox("没有提取到文本。");
    }
}
