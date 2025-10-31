let elementoEnContexto = null;

// --- BASE DE DATOS FALSA (para simular consultas) ---


document.addEventListener('DOMContentLoaded', () => {

    



    // --- 2. LÓGICA DE MODALES (ABRIR/CERRAR) ---
    const openModal = (modalId) => {
        const modal = document.getElementById(modalId);
        if (modal) modal.classList.add('active');
    };
    const closeModal = (modal) => {
        if (modal) modal.classList.remove('active');
    };

    document.getElementById('btn-abrir-modal-cliente')?.addEventListener('click', () => openModal('modal-nuevo-cliente'));

    document.querySelectorAll('.modal-close').forEach(btn => {
        btn.addEventListener('click', (e) => closeModal(e.target.closest('.modal-container')));
    });

    // --- 3. LÓGICA DE PESTAÑAS (TABS) EN ADMINISTRACIÓN ---
    const adminTabContainer = document.getElementById('admin-tab-buttons');
    if (adminTabContainer) {
        adminTabContainer.addEventListener('click', (e) => {
            if (e.target.tagName === 'BUTTON') {
                const targetTab = e.target.getAttribute('data-tab');
                if (!targetTab) return;

                document.querySelectorAll('.tab-content').forEach(tab => tab.classList.remove('active'));
                document.getElementById(targetTab).classList.add('active');

                adminTabContainer.querySelectorAll('button').forEach(btn => {
                    btn.classList.remove('active', 'btn-primary');
                    btn.classList.add('btn-secondary');
                });

                e.target.classList.add('active', 'btn-primary');
                e.target.classList.remove('btn-secondary');
            }
        });
    }

    // --- 4. LÓGICA "REGISTRAR VENTA" ---
    const btnAnadirVenta = document.getElementById('btn-anadir-producto');
    const tablaVentaBody = document.getElementById('tabla-detalle-venta-body');
    const montoTotalInput = document.getElementById('monto-total');
    const montoPagoInput = document.getElementById('monto-pago');
    const montoCambioInput = document.getElementById('monto-cambio');
    const btnRegistrarVentaFinal = document.getElementById('btn-registrar-venta-final');

    // Función para actualizar total de VENTA
    const actualizarTotalVenta = () => {
        let montoTotal = 0;
        if (tablaVentaBody) {
            tablaVentaBody.querySelectorAll('tr').forEach(fila => {
                const celdaSubtotal = fila.cells[5];
                if (celdaSubtotal) {
                    const subtotal = parseFloat(celdaSubtotal.textContent.replace('S/ ', '').replace(',', ''));
                    if (!isNaN(subtotal)) montoTotal += subtotal;
                }
            });
        }
        if (montoTotalInput) {
            montoTotalInput.value = `S/ ${montoTotal.toFixed(2)}`;
        }
        actualizarCambio(); // Actualiza el cambio cada vez que el total cambia
    };

    // Función para actualizar el CAMBIO
    const actualizarCambio = () => {
        if (!montoTotalInput || !montoPagoInput || !montoCambioInput) return;

        const total = parseFloat(montoTotalInput.value.replace('S/ ', '').replace(',', '')) || 0;
        const pago = parseFloat(montoPagoInput.value) || 0;
        const cambio = pago - total;

        montoCambioInput.value = `S/ ${cambio > 0 ? cambio.toFixed(2) : '0.00'}`;
    };

    // Añadir producto a la VENTA
    if (btnAnadirVenta) {
        btnAnadirVenta.addEventListener('click', () => {
            const producto = document.getElementById('sim-producto').value;
            const lote = document.getElementById('sim-lote').value;
            const venc = document.getElementById('sim-venc').value;
            const precio = parseFloat(document.getElementById('sim-precio').value);
            const cantidad = parseInt(document.getElementById('sim-cantidad').value);

            if (!producto || !lote || !venc || isNaN(precio) || isNaN(cantidad) || cantidad <= 0 || precio < 0) {
                alert('Por favor, complete todos los campos del producto con valores válidos.');
                return;
            }

            const subtotal = (precio * cantidad).toFixed(2);
            const nuevaFila = document.createElement('tr');
            nuevaFila.innerHTML = `
                        <td>${producto}</td>
                        <td>${lote}</td>
                        <td>${venc}</td>
                        <td>${cantidad}</td>
                        <td>S/ ${precio.toFixed(2)}</td>
                        <td>S/ ${subtotal}</td>
                        <td><button class="btn-icon btn-danger btn-delete-row">🗑️</button></td>
                    `;
            if (tablaVentaBody) tablaVentaBody.appendChild(nuevaFila);
            actualizarTotalVenta();
        });
    }

    // Calcular cambio en vivo
    if (montoPagoInput) montoPagoInput.addEventListener('input', actualizarCambio);

    // Simular registro final de VENTA
    if (btnRegistrarVentaFinal) {
        btnRegistrarVentaFinal.addEventListener('click', () => {
            if (!tablaVentaBody || tablaVentaBody.rows.length === 0) {
                alert('No hay productos en la venta.');
                return;
            }
            alert('Venta registrada con éxito.');
            // Limpiar formulario
            tablaVentaBody.innerHTML = '';
            if (montoPagoInput) montoPagoInput.value = '';
            if (montoCambioInput) montoCambioInput.value = 'S/ 0.00';
            if (montoTotalInput) montoTotalInput.value = 'S/ 0.00';
            document.getElementById('venta-cliente-razon-social').value = '';
            document.getElementById('venta-cliente-documento').value = '';
            document.getElementById('venta-cliente-direccion').value = '';
        });
    }

    // --- 5. LÓGICA "REGISTRAR COMPRA" ---
    const btnAnadirCompra = document.getElementById('btn-anadir-compra');
    const tablaCompraBody = document.getElementById('tabla-detalle-compra-body');
    const compraTotalSummary = document.getElementById('compra-total-summary');

    // Función para actualizar total de COMPRA
    const actualizarTotalCompra = () => {
        let montoTotal = 0;
        if (tablaCompraBody) {
            tablaCompraBody.querySelectorAll('tr').forEach(fila => {
                const celdaSubtotal = fila.cells[6];
                if (celdaSubtotal) {
                    const subtotal = parseFloat(celdaSubtotal.textContent.replace('S/ ', '').replace(',', ''));
                    if (!isNaN(subtotal)) montoTotal += subtotal;
                }
            });
        }
        if (compraTotalSummary) {
            compraTotalSummary.textContent = `Monto Total Compra: S/ ${montoTotal.toFixed(2)}`;
        }
    };

    // Añadir producto a la COMPRA
    if (btnAnadirCompra) {
        btnAnadirCompra.addEventListener('click', () => {
            const producto = document.getElementById('sim-compra-producto').value;
            const lote = document.getElementById('sim-compra-lote').value;
            const venc = document.getElementById('sim-compra-venc').value;
            const cantidad = parseInt(document.getElementById('sim-compra-cantidad').value);
            const precioCompra = parseFloat(document.getElementById('sim-compra-precio').value);
            const precioVenta = parseFloat(document.getElementById('sim-compra-precioventa').value);

            if (!producto || !lote || !venc || isNaN(cantidad) || isNaN(precioCompra) || isNaN(precioVenta) || cantidad <= 0 || precioCompra < 0) {
                alert('Por favor, complete todos los campos del lote con valores válidos.');
                return;
            }

            const subtotal = (precioCompra * cantidad).toFixed(2);
            const nuevaFila = document.createElement('tr');
            nuevaFila.innerHTML = `
                        <td>${producto}</td>
                        <td>${lote}</td>
                        <td>${venc}</td>
                        <td>${cantidad}</td>
                        <td>S/ ${precioCompra.toFixed(2)}</td>
                        <td>S/ ${precioVenta.toFixed(2)}</td>
                        <td>S/ ${subtotal}</td>
                        <td><button class="btn-icon btn-danger btn-delete-row">🗑️</button></td>
                    `;
            if (tablaCompraBody) tablaCompraBody.appendChild(nuevaFila);
            actualizarTotalCompra();
        });
    }


    // --- 6. LÓGICA DE SIMULACIÓN (CLIENTES, PROVEEDORES, etc.) ---

    // Guardar Nuevo Cliente (desde el modal)
    const btnGuardarCliente = document.getElementById('btn-guardar-cliente');
    if (btnGuardarCliente) {
        btnGuardarCliente.addEventListener('click', () => {
            const doc = document.getElementById('modal-cliente-doc').value;
            const razon = document.getElementById('modal-cliente-razon').value;
            const comercial = document.getElementById('modal-cliente-comercial').value;
            const direccion = document.getElementById('modal-cliente-direccion').value;
            const tel = document.getElementById('modal-cliente-tel').value;

            if (!doc || !razon || !direccion) {
                alert('Documento, Razón Social y Dirección son obligatorios.');
                return;
            }

            const tablaClientesBody = document.getElementById('tabla-clientes-body');
            const nuevaFila = document.createElement('tr');
            nuevaFila.innerHTML = `
                        <td>${doc}</td>
                        <td>${razon}</td>
                        <td>${comercial}</td>
                        <td>${direccion}</td>
                        <td>${tel}</td>
                        <td class="status-active">● Activo</td>
                        <td class="table-actions">
                            <button class="btn-icon" title="Editar">✏️</button>
                            <button class="btn-icon btn-icon-red btn-desactivar" data-type="cliente" title="Desactivar">❌</button>
                        </td>
                    `;
            if (tablaClientesBody) tablaClientesBody.appendChild(nuevaFila);

            // Limpiar formulario y cerrar modal
            document.getElementById('form-nuevo-cliente').reset();
            closeModal(document.getElementById('modal-nuevo-cliente'));
        });
    }

    // Lógica de Desactivar (y Activar)
    const btnConfirmarAccionFinal = document.getElementById('btn-confirmar-accion-final');
    if (btnConfirmarAccionFinal) {
        btnConfirmarAccionFinal.addEventListener('click', () => {
            if (elementoEnContexto) {
                const fila = elementoEnContexto.closest('tr');

                let celdaEstado;
                let celdaAccion;

                if (fila.cells.length > 5) { // Tabla de Clientes (7 celdas)
                    celdaEstado = fila.cells[5];
                    celdaAccion = fila.cells[6];
                } else { // Proveedores o Usuarios (6 celdas)
                    celdaEstado = fila.cells[4];
                    celdaAccion = fila.cells[5];
                }

                if (elementoEnContexto.classList.contains('btn-desactivar')) {
                    // Desactivar
                    celdaEstado.innerHTML = '● Inactivo';
                    celdaEstado.className = 'status-inactive';
                    celdaAccion.innerHTML = `
                                <button class="btn-icon" title="Editar">✏️</button>
                                <button class="btn-icon btn-icon-green btn-activar" data-type="${elementoEnContexto.dataset.type}" title="Activar">✔️</button>
                            `;
                } else if (elementoEnContexto.classList.contains('btn-activar')) {
                    // Activar
                    celdaEstado.innerHTML = '● Activo';
                    celdaEstado.className = 'status-active';
                    celdaAccion.innerHTML = `
                                <button class="btn-icon" title="Editar">✏️</button>
                                <button class="btn-icon btn-icon-red btn-desactivar" data-type="${elementoEnContexto.dataset.type}" title="Desactivar">❌</button>
                            `;
                }
            }
            closeModal(document.getElementById('modal-confirmar-accion'));
            elementoEnContexto = null;
        });
    }


    // --- 7. DELEGACIÓN DE EVENTOS GLOBAL (para botones en tablas) ---
    const contentWrapper = document.querySelector('.content-wrapper');
    if (contentWrapper) {
        contentWrapper.addEventListener('click', (e) => {
            const target = e.target.closest('button');
            if (!target) return;

            // Lógica para ELIMINAR FILA (Venta y Compra)
            if (target.classList.contains('btn-delete-row')) {
                target.closest('tr').remove();
                if (document.getElementById('vista-registrar-venta').classList.contains('active')) {
                    actualizarTotalVenta();
                } else if (document.getElementById('vista-registrar-compra').classList.contains('active')) {
                    actualizarTotalCompra();
                }
            }

            // Lógica para ABRIR MODAL DE CONFIRMACIÓN (Desactivar/Activar)
            if (target.classList.contains('btn-desactivar') || target.classList.contains('btn-activar')) {
                elementoEnContexto = target;
                const mensaje = target.classList.contains('btn-desactivar') ? '¿Está seguro de que desea DESACTIVAR este elemento?' : '¿Está seguro de que desea ACTIVAR este elemento?';
                const btnConfirmar = document.getElementById('btn-confirmar-accion-final');
                const modalMensaje = document.getElementById('modal-confirmar-mensaje');

                if (modalMensaje) modalMensaje.textContent = mensaje;
                if (btnConfirmar) {
                    btnConfirmar.className = target.classList.contains('btn-desactivar') ? 'btn btn-danger' : 'btn btn-success';
                    btnConfirmar.textContent = target.classList.contains('btn-desactivar') ? 'Desactivar' : 'Activar';
                }

                openModal('modal-confirmar-accion');
            }

            // Lógica para VER DETALLE DE VENTA
            if (target.classList.contains('btn-ver-detalle')) {
                const ventaId = target.dataset.idVenta;
                const data = fakeDatabase.ventas[ventaId];

                if (data) {
                    document.getElementById('modal-detalle-venta-titulo').textContent = `Detalle de Venta - ${data.nro}`;
                    document.getElementById('detalle-venta-nro').textContent = data.nro;
                    document.getElementById('detalle-venta-fecha').textContent = data.fecha;
                    document.getElementById('detalle-venta-usuario').textContent = data.usuario;
                    document.getElementById('detalle-cliente-razon').textContent = data.cliente.razon;
                    document.getElementById('detalle-cliente-doc').textContent = data.cliente.doc;
                    document.getElementById('detalle-cliente-dir').textContent = data.cliente.dir;

                    const tablaBody = document.getElementById('modal-detalle-venta-tabla');
                    tablaBody.innerHTML = '';
                    data.items.forEach(item => {
                        const fila = document.createElement('tr');
                        fila.innerHTML = `
                                    <td>${item.prod}</td>
                                    <td>${item.lote}</td>
                                    <td>${item.venc}</td>
                                    <td>${item.cant}</td>
                                    <td>S/ ${item.precio}</td>
                                    <td>S/ ${item.sub}</td>
                                `;
                        tablaBody.appendChild(fila);
                    });

                    document.getElementById('detalle-venta-total').textContent = `Monto Total: ${data.total}`;
                    openModal('modal-detalle-venta');
                }
            }
        });
    }

    // --- 8. BOTÓN DE IMPRESIÓN ---
    const btnImprimir = document.getElementById('btn-imprimir-venta');
    if (btnImprimir) {
        btnImprimir.addEventListener('click', () => {
            const nroDoc = document.getElementById('detalle-venta-nro').textContent;
            alert(`Simulando impresión de ${nroDoc}...`);
        });
    }

    // --- 9. INICIALIZACIÓN ---
    actualizarTotalVenta();
    actualizarTotalCompra();

});