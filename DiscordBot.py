//Discord Bot to retrieve a link from the Steam Workshop based on the command

import requests
from bs4 import BeautifulSoup
import discord
from discord.ext import commands
from discord_slash import SlashCommand

BOT_TOKEN = ***;
Channel_ID = ***;
guildID = ***;

intents = discord.Intents.default()
bot = commands.Bot(command_prefix='w.', intents = intents);
slash = SlashCommand(bot, sync_commands=True)


@bot.event
async def on_ready():
   await bot.change_presence(activity=discord.Game('with Josephs Nuts'));
   print("BOT IS ACTIVE");
   await slash.sync_all_commands()

@bot.command(name="topweekly", description="Gets the top workshop map for the week")
async def topweekly(ctx):
    topSevenUrl = "https://steamcommunity.com/workshop/browse/?appid=311210&browsesort=trend&section=readytouseitems&actualsort=trend&p=1&days=7"
    r = requests.get(topSevenUrl)
    data = r.text
    soup = BeautifulSoup(data, 'html.parser')
    list = ''

    for topSevenUrl in soup.find_all('a'):
        list += str(topSevenUrl.get('href'))+ '\n';

    count = 0;
    otherCount = 0;
    topSevenUrl = '';

    for item in list:
        if (item == '#'):
            count += 1
            continue;
        if (count == 2):
            #print(item)
            topSevenUrl += item;
            if (item == '\n'):
                otherCount += 1;
                continue;
            if(otherCount == 2):
                break;

    topSevenUrl = topSevenUrl[ : -2];
    #print("\n\n\n\nLINK: " + list);
    #print("LINK: " + topSevenUrl);
    channel = bot.get_channel(Channel_ID);
    await bot.change_presence(activity=discord.Game('with Josephs Nuts'));
    print(topSevenUrl);
    await channel.send(topSevenUrl);


@bot.command(name="toptoday", description="Gets the top workshop map of the day")
async def topday(ctx):
    topDayUrl = "https://steamcommunity.com/workshop/browse/?appid=311210&browsesort=trend&section=readytouseitems&actualsort=trend&p=1&days=1"
    r = requests.get(topDayUrl);
    data = r.text;
    soup = BeautifulSoup(data, 'html.parser');
    list = ''

    for topDayUrl in soup.find_all('a'):
        list += str(topDayUrl.get('href'))+ '\n';

    count = 0;
    otherCount = 0;
    topDayUrl = '';

    for item in list:
        if (item == '#'):
            count += 1
            continue;
        if (count == 2):
            #print(item)
            topDayUrl += item;
            if (item == '\n'):
                otherCount += 1;
                continue;
            if(otherCount == 2):
                break;

    topDayUrl = topDayUrl[ : -2];
    #print("\n\n\n\nLINK: " + list);
    #print("LINK: " + topDayUrl);
    channel = bot.get_channel(Channel_ID);
    print(topDayUrl);
    await channel.send(topDayUrl);


@bot.command(name="topofalltime", description="Gets the top workshop map of all time")
async def TopAllTime(ctx):
    topAllTime = "https://steamcommunity.com/workshop/browse/?appid=311210&browsesort=trend&section=readytouseitems&actualsort=trend&p=1&days=-1"
    topList = ''
    r = requests.get(topAllTime)
    data = r.text;
    soup = BeautifulSoup(data, 'html.parser');

    for topAllTime in soup.find_all('a'):
        topList += str(topAllTime.get('href'))+ '\n';

    count = 0;
    otherCount = 0;
    topAllTime = '';
    #print(topList);
    for item in topList:
        if (item == '#'):
            count += 1
            continue;
        if (count == 2):
            #print(item)
            topAllTime += item;
            if (item == '\n'):
                otherCount += 1;
                continue;
            if(otherCount == 2):
                break;

    topAllTime = topAllTime[ : -2];
    #print("\n\n\n\nLINK: " + topList);
    #print("LINK: " + topAllTime);
    channel = bot.get_channel(Channel_ID);
    print(topAllTime);
    await channel.send(topAllTime);
   
bot.run(BOT_TOKEN);
