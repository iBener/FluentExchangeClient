using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinancePerpetualResponseAccount
{
    public decimal totalInitialMargin;
    public decimal totalMaintMargin;
    public decimal totalWalletBalance;
    public decimal totalUnrealizedProfit;
    public decimal totalMarginBalance;
    public decimal totalPositionInitialMargin;
    public decimal totalOpenOrderInitialMargin;
    public decimal totalCrossWalletBalance;
    public decimal totalCrossUnPnl;
    public decimal availableBalance;
    public decimal maxWithdrawAmount;
    [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
    public DateTimeOffset updateTime;
    public List<BinancePerpetualResponseAccountAssets> assets;
    public List<BinancePerpetualResponseAccountPositions> positions;
}

class BinancePerpetualResponseAccountAssets
{
    public string asset;
    public decimal walletBalance;
    public decimal unrealizedProfit;
    public decimal marginBalance;
    public decimal maintMargin;
    public decimal initialMargin;
    public decimal positionInitialMargin;
    public decimal openOrderInitialMargin;
    public decimal maxWithdrawAmount;
    public decimal crossWalletBalance;
    public decimal crossUnPnl;
    public decimal availableBalance;
    public bool marginAvailable;
    [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
    public DateTimeOffset updateTime;
}

class BinancePerpetualResponseAccountPositions
{
    public string symbol;
    public decimal initialMargin;
    public decimal maintMargin;
    public decimal unrealizedProfit;
    public decimal positionInitialMargin;
    public decimal openOrderInitialMargin;
    public decimal leverage;
    public bool isolated;
    public decimal entryPrice;
    public decimal maxNotional;
    public string positionSide;
    public decimal positionAmt;
    public decimal notional;
    public decimal isolatedWallet;
    [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
    public DateTimeOffset updateTime;
    public decimal bidNotional;
    public decimal askNotional;
}
