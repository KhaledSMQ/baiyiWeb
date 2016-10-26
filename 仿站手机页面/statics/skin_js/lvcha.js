lvMap = window.lvMap || {};
function lvViaJs(locationId) {
    var _f = undefined;
    var _fconv = 'lvMap[\"' + locationId + '\"]';
    try {
        _f = eval(_fconv);
        if (_f != undefined) {
            _f()
        }
    } catch(e) {}
}
function lvLoader(closetag) {
    var lvTest = null,
    lvTestPos = document.getElementsByTagName("span");
    for (var i = 0; i < lvTestPos.length; i++) {
        if (lvTestPos[i].className == "lvTestPos") {
	
            lvTest = lvTestPos[i];
            break
        }
    }
    if (lvTest == null) return;
    if (!closetag) {
        document.write("<span id=lvTestPos_" + lvTest.id + " style=display:none>");
        lvViaJs(lvTest.id);
        return
    }
    document.write("</span>");
    var real = document.getElementById("lvTestPos_" + lvTest.id);
    for (var i = 0; i < real.childNodes.length; i++) {
        var node = real.childNodes[i];
        if (node.tagName == "SCRIPT" && /closetag/.test(node.className)) continue;
        lvTest.parentNode.insertBefore(node, lvTest);
        i--
    }
    lvTest.parentNode.removeChild(lvTest);
    real.parentNode.removeChild(real)
}
lvMap['1'] = function() {
	
};
lvMap['2'] = function() {
document.writeln("<script type=\"text/javascript\">/*33lc-20:3*/var cpro_id = \"u1467457\";</script>");
document.writeln("<script src=\"../../../cpro.baidustatic.com/cpro/ui/cm.js\" type=\"text/javascript\"></script>");
};
lvMap['3'] = function() {
document.writeln("<script type=\"text/javascript\">");
document.writeln("    /*33lc-20:3*/");
document.writeln("    var cpro_id = \"u1467461\";</script>");
document.writeln("<script src=\"../../../cpro.baidustatic.com/cpro/ui/cm.js\" type=\"text/javascript\"></script>");
};