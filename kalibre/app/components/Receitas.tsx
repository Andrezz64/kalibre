"use client";
import { Pencil, Trash } from "@phosphor-icons/react/dist/ssr";

import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { useState } from "react";
import { Calendar, CurrencyCircleDollar } from "@phosphor-icons/react";

import ReceitaEditForm from "./ReceitaEditForm";
import FormatStringBrasil from "./FormatStringBrasil";
import Data from "../Data";

export default function Receitas(props: any) {
  const NovaInstanciaData = new Date(props.data);
  const DataFormatada = NovaInstanciaData.toLocaleString("pt-BR");

  const [openModal, setOpenModal] = useState(false);

  const style = {
    position: "absolute" as "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: 400,
    bgcolor: "background.paper",
    border: "2px solid #000",
    boxShadow: 24,
    p: 4,
  };

  const handleClickOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const handleOpenModal = () => setOpenModal(true);
  const handleCloseModal = () => setOpenModal(false);

  const Delete = () => {
    const options = {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    };

    fetch(
      `http://${Data.FetchIp}/api/v1/receitas/` + props.receitaId,
      options
    )
      .then((response) => response.json())
      .then((response) => console.log(response))
      .catch((err) => console.error(err));
   
    location.reload();
  };

  const [open, setOpen] = useState(false);

  return (
    <div className="bg-stone-200 max-w-[14rem] max-h-[10rem] p-2 rounded-lg shadow-lg">
      <h2 className="flex items-center">
        <Calendar size={26} />
        {DataFormatada}
      </h2>
      <h2>
        <span className="text-green-500 flex items-center">
          <CurrencyCircleDollar size={26} />
          <FormatStringBrasil valor={props.valor} />
        </span>
      </h2>
      <div className="flex gap-3 mt-1">
        <Button variant="outlined" onClick={handleClickOpen}>
          <Trash size={26} color="#ce2c2c" />
        </Button>
        <Dialog
          open={open}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
        >
          <DialogTitle id="alert-dialog-title">
            {"Tem certeza disso ?"}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              Você está prestes a excluir essa receita, essa ação não pode ser
              desfeita
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose}>Cancelar</Button>
            <Button
              onClick={Delete}
              className="text-red-500"
              autoFocus
              sx={{
                color: "red",
              }}
            >
              Excluir
            </Button>
          </DialogActions>
        </Dialog>
        <Button onClick={handleOpenModal}>
          <Pencil size={26} color="#1a1a1a" />
        </Button>
        <Modal
          open={openModal}
          onClose={handleCloseModal}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Editar receita
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              <ReceitaEditForm
                receitaId={props.receitaId}
                valor={props.valor}
                data={props.data}
              ></ReceitaEditForm>
            </Typography>
          </Box>
        </Modal>
      </div>
    </div>
  );
}
