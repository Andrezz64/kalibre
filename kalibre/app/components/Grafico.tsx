import { BarChart, cheerfulFiestaPaletteDark } from "@mui/x-charts";

interface Grafico {
  label: string;
  data: Array<number>;
  color: string;
  _label?: string;
  _data?: Array<number>;
}

export default function Grafico(props: Grafico) {
  const mesesDoAno = [
    "Janeiro",
    "Fevereiro",
    "Março",
    "Abril",
    "Maio",
    "Junho",
    "Julho",
    "Agosto",
    "Setembro",
    "Outubro",
    "Novembro",
    "Dezembro",
  ];

  return (
    <BarChart
      xAxis={[{ scaleType: "band", data: mesesDoAno }]}
      series={[
        {
          label: props.label,
          data: props.data,
          color: props.color,
        },
        {
          label: props._label,
          data: props._data,
          color: "red",
        },
      ]}
      width={500}
      height={300}
      grid={{ vertical: true, horizontal: true }}
      colors={cheerfulFiestaPaletteDark}
    />
  );
}
