$(document).ready(function () {

	inicializa();

	$('#cep').mask('99999-999');

	$('#documento').keyup(function () {
		var valor = getOnlyNumber($('#documento').val());
		// limpa máscara se o campo estiver vazio
		if (valor.length == 0)
			$('#documento').unmask();
	});

	$('#documento').blur(function () {
		var valor = getOnlyNumber($(this).val());
		$('#documento').unmask();
		if (valor.length == 11)
			$('#documento').mask('999.999.999-99');
		else if (valor.length > 11)
			$('#documento').mask('99.999.999/9999-99');
	});

	$("#cep").blur(function () {
		// cria nova variável 'cep' somente com digitos
		var cep = getOnlyNumber($(this).val()); //$(this).val().replace(/\D/g, '');

		if (cep != "") {
			consultaCEP(cep)
		}
		else {
			limpa_formulario_cep();
		}
	});

	function inicializa() {
		var cep = getOnlyNumber($("#cep").val()); //$("#cep").val().replace(/\D/g, '');

		if (cep != "") {
			consultaCEP(cep);
		}
	}

	function getOnlyNumber(value) {
		return value.replace(/\D/g, '');
	};

	function limpa_formulario_cep() {
		$("#cep").val("");
		$("#rua").val("");
		$("#bairro").val("");
		$("#cidade").val("");
		$("#estado").val("");
	}

	function consultaCEP(cep) {
		// expressao regular para validar o CEP
		var validaCepReg = /^[0-9]{8}$/;

		if (validaCepReg.test(cep)) {
			// preenche os campos com '...' enquanto consulta o webservice
			$("#rua").val("...");
			$("#bairro").val("...");
			$("#cidade").val("...");
			$("#estado").val("...");

			$.ajax({
				type: "GET",
				url: "https://brasilapi.com.br/api/cep/v1/" + cep,
				success: function (dados) {
					$("#rua").val(dados.street);
					$("#bairro").val(dados.neighborhood);
					$("#cidade").val(dados.city);
					$("#estado").val(dados.state);
				},
				error: function (err) {
					alert(err.responseJSON.errors[0].message);
					limpa_formulario_cep();
				}
			});
		}
		else {
			limpa_formulario_cep();
			alert('formato de CEP inválido');
		}

	}

});
