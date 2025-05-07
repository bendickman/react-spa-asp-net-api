const config = {
    baseApiUrl: ""
}

const currencyFormatter = Intl.NumberFormat("en-GB", {
    style: "currency",
    currency: "GBP",
    maximumFractionDigits: 0,
});

export default config;
export { currencyFormatter };