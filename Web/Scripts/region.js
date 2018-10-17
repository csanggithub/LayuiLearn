$.fn.region = function () {
	var //$ = layui.jquery,
		form = layui.form;
	
	var areaState = {
		area: {
			state: false,
			data: [],
			fn: function (value) {
				this.state = true;
				areaState.street.state = false;
				areaState.place.state = false;
				var str = GetRegionJson(value);
				areaState.street.data = str;//selDate.street;
				if (value !== '') {
					addSelect('city', areaState.street.data, true, 'RegionCode', 'RegionName');
				} else {
					$('#city').html('');
					$('#area').html('');
					form.render("select");
				}
			}
		},
		street: {
			state: false,
			data: [],
			fn: function (value) {
				this.state = true;
				areaState.place.state = false;
				var str = GetRegionJson(value);
				areaState.place.data = str;
				if (value !== '') {
					addSelect('area', areaState.place.data, true, 'RegionCode', 'RegionName');
				} else {
					$('#area').html('');
					form.render("select");
				}
			}
		},
		place: {
			state: false,
			data: [],
			fn: function () {
				this.state = true;
			}
		}
	};

	function addSelect(id, data, bool, val, text, region) {//bool是否添加“请选择”选项
		val = val || 'type';
		text = text || 'name';
		var d = "'" + region + "'";
		var html = '';
		var $id = $('#' + id);
		$id.html('');
		if (bool) { html += '<option value="">请选择</option>'; }
		var jsonObj = JSON.parse(data);
		for (var i = 0; i < jsonObj.length; i++) {
			html += '<option value="' + jsonObj[i].RegionCode + '">' + jsonObj[i].RegionName + '</option>';
		}
		$id.html(html);
		form.render("select");
	}
	var provinceData = GetRegionJson(0);
	addSelect('province', provinceData, true, 'RegionCode', 'RegionName');

	// 一级联动事件触发
	form.on('select(province)', function (data) {
		var value = data.value;
		areaState.area.fn(value);
		areaState.street.fn(value);
	});

	// 二级联动事件触发
	form.on('select(city)', function (data) {
		var value = data.value;
		areaState.street.fn(value);
	});
}
function GetRegionJson(areaCode) {
	var jsonStr;
	$.ajax({
		type: "POST",
		url: "GetRegionJson",//'@Url.Action("GetAreaList","User")',
		data: { areaCode: areaCode },
		async: false,
		datatype: "json",//"xml", "html", "script", "json", "jsonp", "text".
		beforeSend: function () { },
		success: function (data) {
			jsonStr = data;
		},
		complete: function (XMLHttpRequest, textStatus) {
			//alert(XMLHttpRequest.responseText);
			//alert(textStatus);
		},
		error: function () {
		}
	});
	return jsonStr;
}