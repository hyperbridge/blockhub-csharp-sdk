// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using UnityEngine;

namespace DoozyUI.FontAwesome
{
    public static class FA
    {
        public const string VERSION = "1.1.0";
        public const string FONT_VERSION = "4.7";
        public const string COPYRIGHT = "Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.";
        public const string SYMBOL_FONT_AWESOME = "dUI_FontAwesome";

        public static Dictionary<string, string> AllIcons = new Dictionary<string, string>()
        {
            {"Px500",px500},
            {"Address Book",address_book},
            {"Address Book O",address_book_o},
            {"Address Card",address_card},
            {"Address Card O",address_card_o},
            {"Adjust",adjust},
            {"Adn",adn},
            {"Align Center",align_center},
            {"Align Justify",align_justify},
            {"Align Left",align_left},
            {"Align Right",align_right},
            {"Amazon",amazon},
            {"Ambulance",ambulance},
            {"American Sign Language Interpreting",american_sign_language_interpreting},
            {"Anchor",anchor},
            {"Android",android},
            {"Angellist",angellist},
            {"Angle Double Down",angle_double_down},
            {"Angle Double Left",angle_double_left},
            {"Angle Double Right",angle_double_right},
            {"Angle Double Up",angle_double_up},
            {"Angle Down",angle_down},
            {"Angle Left",angle_left},
            {"Angle Right",angle_right},
            {"Angle Up",angle_up},
            {"Apple",apple},
            {"Archive",archive},
            {"Area Chart",area_chart},
            {"Arrow Circle Down",arrow_circle_down},
            {"Arrow Circle Left",arrow_circle_left},
            {"Arrow Circle O Down",arrow_circle_o_down},
            {"Arrow Circle O Left",arrow_circle_o_left},
            {"Arrow Circle O Right",arrow_circle_o_right},
            {"Arrow Circle O Up",arrow_circle_o_up},
            {"Arrow Circle Right",arrow_circle_right},
            {"Arrow Circle Up",arrow_circle_up},
            {"Arrow Down",arrow_down},
            {"Arrow Left",arrow_left},
            {"Arrow Right",arrow_right},
            {"Arrow Up",arrow_up},
            {"Arrows",arrows},
            {"Arrows Alt",arrows_alt},
            {"Arrows H",arrows_h},
            {"Arrows V",arrows_v},
            {"Asl Interpreting Alias",asl_interpreting_alias},
            {"Assistive Listening Systems",assistive_listening_systems},
            {"Asterisk",asterisk},
            {"At",at},
            {"Audio Description",audio_description},
            {"Automobile Alias",automobile_alias},
            {"Backward",backward},
            {"Balance Scale",balance_scale},
            {"Ban",ban},
            {"Bandcamp",bandcamp},
            {"Bank Alias",bank_alias},
            {"Bar Chart",bar_chart},
            {"Bar Chart O Alias",bar_chart_o_alias},
            {"Barcode",barcode},
            {"Bars",bars},
            {"Bath",bath},
            {"Bathtub Alias",bathtub_alias},
            {"Battery Alias",battery_alias},
            {"Battery 0 Alias",battery_0_alias},
            {"Battery 1 Alias",battery_1_alias},
            {"Battery 2 Alias",battery_2_alias},
            {"Battery 3 Alias",battery_3_alias},
            {"Battery 4 Alias",battery_4_alias},
            {"Battery Empty",battery_empty},
            {"Battery Full",battery_full},
            {"Battery Half",battery_half},
            {"Battery Quarter",battery_quarter},
            {"Battery Three Quarters",battery_three_quarters},
            {"Bed",bed},
            {"Beer",beer},
            {"Behance",behance},
            {"Behance Square",behance_square},
            {"Bell",bell},
            {"Bell O",bell_o},
            {"Bell Slash",bell_slash},
            {"Bell Slash O",bell_slash_o},
            {"Bicycle",bicycle},
            {"Binoculars",binoculars},
            {"Birthday Cake",birthday_cake},
            {"Bitbucket",bitbucket},
            {"Bitbucket Square",bitbucket_square},
            {"Bitcoin Alias",bitcoin_alias},
            {"Black Tie",black_tie},
            {"Blind",blind},
            {"Bluetooth",bluetooth},
            {"Bluetooth B",bluetooth_b},
            {"Bold",bold},
            {"Bolt",bolt},
            {"Bomb",bomb},
            {"Book",book},
            {"Bookmark",bookmark},
            {"Bookmark O",bookmark_o},
            {"Braille",braille},
            {"Briefcase",briefcase},
            {"Btc",btc},
            {"Bug",bug},
            {"Building",building},
            {"Building O",building_o},
            {"Bullhorn",bullhorn},
            {"Bullseye",bullseye},
            {"Bus",bus},
            {"Buysellads",buysellads},
            {"Cab Alias",cab_alias},
            {"Calculator",calculator},
            {"Calendar",calendar},
            {"Calendar Check O",calendar_check_o},
            {"Calendar Minus O",calendar_minus_o},
            {"Calendar O",calendar_o},
            {"Calendar Plus O",calendar_plus_o},
            {"Calendar Times O",calendar_times_o},
            {"Camera",camera},
            {"Camera Retro",camera_retro},
            {"Car",car},
            {"Caret Down",caret_down},
            {"Caret Left",caret_left},
            {"Caret Right",caret_right},
            {"Caret Square O Down",caret_square_o_down},
            {"Caret Square O Left",caret_square_o_left},
            {"Caret Square O Right",caret_square_o_right},
            {"Caret Square O Up",caret_square_o_up},
            {"Caret Up",caret_up},
            {"Cart Arrow Down",cart_arrow_down},
            {"Cart Plus",cart_plus},
            {"Cc",cc},
            {"Cc Amex",cc_amex},
            {"Cc Diners Club",cc_diners_club},
            {"Cc Discover",cc_discover},
            {"Cc Jcb",cc_jcb},
            {"Cc Mastercard",cc_mastercard},
            {"Cc Paypal",cc_paypal},
            {"Cc Stripe",cc_stripe},
            {"Cc Visa",cc_visa},
            {"Certificate",certificate},
            {"Chain Alias",chain_alias},
            {"Chain Broken",chain_broken},
            {"Check",check},
            {"Check Circle",check_circle},
            {"Check Circle O",check_circle_o},
            {"Check Square",check_square},
            {"Check Square O",check_square_o},
            {"Chevron Circle Down",chevron_circle_down},
            {"Chevron Circle Left",chevron_circle_left},
            {"Chevron Circle Right",chevron_circle_right},
            {"Chevron Circle Up",chevron_circle_up},
            {"Chevron Down",chevron_down},
            {"Chevron Left",chevron_left},
            {"Chevron Right",chevron_right},
            {"Chevron Up",chevron_up},
            {"Child",child},
            {"Chrome",chrome},
            {"Circle",circle},
            {"Circle O",circle_o},
            {"Circle O Notch",circle_o_notch},
            {"Circle Thin",circle_thin},
            {"Clipboard",clipboard},
            {"Clock O",clock_o},
            {"Clone",clone},
            {"Close Alias",close_alias},
            {"Cloud",cloud},
            {"Cloud Download",cloud_download},
            {"Cloud Upload",cloud_upload},
            {"Cny Alias",cny_alias},
            {"Code",code},
            {"Code Fork",code_fork},
            {"Codepen",codepen},
            {"Codiepie",codiepie},
            {"Coffee",coffee},
            {"Cog",cog},
            {"Cogs",cogs},
            {"Columns",columns},
            {"Comment",comment},
            {"Comment O",comment_o},
            {"Commenting",commenting},
            {"Commenting O",commenting_o},
            {"Comments",comments},
            {"Comments O",comments_o},
            {"Compass",compass},
            {"Compress",compress},
            {"Connectdevelop",connectdevelop},
            {"Contao",contao},
            {"Copy Alias",copy_alias},
            {"Copyright",copyright},
            {"Creative Commons",creative_commons},
            {"Credit Card",credit_card},
            {"Credit Card Alt",credit_card_alt},
            {"Crop",crop},
            {"Crosshairs",crosshairs},
            {"Css3",css3},
            {"Cube",cube},
            {"Cubes",cubes},
            {"Cut Alias",cut_alias},
            {"Cutlery",cutlery},
            {"Dashboard Alias",dashboard_alias},
            {"Dashcube",dashcube},
            {"Database",database},
            {"Deaf",deaf},
            {"Deafness Alias",deafness_alias},
            {"Dedent Alias",dedent_alias},
            {"Delicious",delicious},
            {"Desktop",desktop},
            {"Deviantart",deviantart},
            {"Diamond",diamond},
            {"Digg",digg},
            {"Dollar Alias",dollar_alias},
            {"Dot Circle O",dot_circle_o},
            {"Download",download},
            {"Dribbble",dribbble},
            {"Drivers License Alias",drivers_license_alias},
            {"Drivers License O Alias",drivers_license_o_alias},
            {"Dropbox",dropbox},
            {"Drupal",drupal},
            {"Edge",edge},
            {"Edit Alias",edit_alias},
            {"Eercast",eercast},
            {"Eject",eject},
            {"Ellipsis H",ellipsis_h},
            {"Ellipsis V",ellipsis_v},
            {"Empire",empire},
            {"Envelope",envelope},
            {"Envelope O",envelope_o},
            {"Envelope Open",envelope_open},
            {"Envelope Open O",envelope_open_o},
            {"Envelope Square",envelope_square},
            {"Envira",envira},
            {"Eraser",eraser},
            {"Etsy",etsy},
            {"Eur",eur},
            {"Euro Alias",euro_alias},
            {"Exchange",exchange},
            {"Exclamation",exclamation},
            {"Exclamation Circle",exclamation_circle},
            {"Exclamation Triangle",exclamation_triangle},
            {"Expand",expand},
            {"Expeditedssl",expeditedssl},
            {"External Link",external_link},
            {"External Link Square",external_link_square},
            {"Eye",eye},
            {"Eye Slash",eye_slash},
            {"Eyedropper",eyedropper},
            {"Fa Alias",fa_alias},
            {"Facebook",facebook},
            {"Facebook F Alias",facebook_f_alias},
            {"Facebook Official",facebook_official},
            {"Facebook Square",facebook_square},
            {"Fast Backward",fast_backward},
            {"Fast Forward",fast_forward},
            {"Fax",fax},
            {"Feed Alias",feed_alias},
            {"Female",female},
            {"Fighter Jet",fighter_jet},
            {"File",file},
            {"File Archive O",file_archive_o},
            {"File Audio O",file_audio_o},
            {"File Code O",file_code_o},
            {"File Excel O",file_excel_o},
            {"File Image O",file_image_o},
            {"File Movie O Alias",file_movie_o_alias},
            {"File O",file_o},
            {"File Pdf O",file_pdf_o},
            {"File Photo O Alias",file_photo_o_alias},
            {"File Picture O Alias",file_picture_o_alias},
            {"File Powerpoint O",file_powerpoint_o},
            {"File Sound O Alias",file_sound_o_alias},
            {"File Text",file_text},
            {"File Text O",file_text_o},
            {"File Video O",file_video_o},
            {"File Word O",file_word_o},
            {"File Zip O Alias",file_zip_o_alias},
            {"Files O",files_o},
            {"Film",film},
            {"Filter",filter},
            {"Fire",fire},
            {"Fire Extinguisher",fire_extinguisher},
            {"Firefox",firefox},
            {"First Order",first_order},
            {"Flag",flag},
            {"Flag Checkered",flag_checkered},
            {"Flag O",flag_o},
            {"Flash Alias",flash_alias},
            {"Flask",flask},
            {"Flickr",flickr},
            {"Floppy O",floppy_o},
            {"Folder",folder},
            {"Folder O",folder_o},
            {"Folder Open",folder_open},
            {"Folder Open O",folder_open_o},
            {"Font",font},
            {"Font Awesome",font_awesome},
            {"Fonticons",fonticons},
            {"Fort Awesome",fort_awesome},
            {"Forumbee",forumbee},
            {"Forward",forward},
            {"Foursquare",foursquare},
            {"Free Code Camp",free_code_camp},
            {"Frown O",frown_o},
            {"Futbol O",futbol_o},
            {"Gamepad",gamepad},
            {"Gavel",gavel},
            {"Gbp",gbp},
            {"Ge Alias",ge_alias},
            {"Gear Alias",gear_alias},
            {"Gears Alias",gears_alias},
            {"Genderless",genderless},
            {"Get Pocket",get_pocket},
            {"Gg",gg},
            {"Gg Circle",gg_circle},
            {"Gift",gift},
            {"Git",git},
            {"Git Square",git_square},
            {"Github",github},
            {"Github Alt",github_alt},
            {"Github Square",github_square},
            {"Gitlab",gitlab},
            {"Gittip Alias",gittip_alias},
            {"Glass",glass},
            {"Glide",glide},
            {"Glide G",glide_g},
            {"Globe",globe},
            {"Google",google},
            {"Google Plus",google_plus},
            {"Google Plus Circle Alias",google_plus_circle_alias},
            {"Google Plus Official",google_plus_official},
            {"Google Plus Square",google_plus_square},
            {"Google Wallet",google_wallet},
            {"Graduation Cap",graduation_cap},
            {"Gratipay",gratipay},
            {"Grav",grav},
            {"Group Alias",group_alias},
            {"H Square",h_square},
            {"Hacker News",hacker_news},
            {"Hand Grab O Alias",hand_grab_o_alias},
            {"Hand Lizard O",hand_lizard_o},
            {"Hand O Down",hand_o_down},
            {"Hand O Left",hand_o_left},
            {"Hand O Right",hand_o_right},
            {"Hand O Up",hand_o_up},
            {"Hand Paper O",hand_paper_o},
            {"Hand Peace O",hand_peace_o},
            {"Hand Pointer O",hand_pointer_o},
            {"Hand Rock O",hand_rock_o},
            {"Hand Scissors O",hand_scissors_o},
            {"Hand Spock O",hand_spock_o},
            {"Hand Stop O Alias",hand_stop_o_alias},
            {"Handshake O",handshake_o},
            {"Hard Of Hearing Alias",hard_of_hearing_alias},
            {"Hashtag",hashtag},
            {"Hdd O",hdd_o},
            {"Header",header},
            {"Headphones",headphones},
            {"Heart",heart},
            {"Heart O",heart_o},
            {"Heartbeat",heartbeat},
            {"History",history},
            {"Home",home},
            {"Hospital O",hospital_o},
            {"Hotel Alias",hotel_alias},
            {"Hourglass",hourglass},
            {"Hourglass 1 Alias",hourglass_1_alias},
            {"Hourglass 2 Alias",hourglass_2_alias},
            {"Hourglass 3 Alias",hourglass_3_alias},
            {"Hourglass End",hourglass_end},
            {"Hourglass Half",hourglass_half},
            {"Hourglass O",hourglass_o},
            {"Hourglass Start",hourglass_start},
            {"Houzz",houzz},
            {"Html5",html5},
            {"I Cursor",i_cursor},
            {"Id Badge",id_badge},
            {"Id Card",id_card},
            {"Id Card O",id_card_o},
            {"Ils",ils},
            {"Image Alias",image_alias},
            {"Imdb",imdb},
            {"Inbox",inbox},
            {"Indent",indent},
            {"Industry",industry},
            {"Info",info},
            {"Info Circle",info_circle},
            {"Inr",inr},
            {"Instagram",instagram},
            {"Institution Alias",institution_alias},
            {"Internet Explorer",internet_explorer},
            {"Intersex Alias",intersex_alias},
            {"Ioxhost",ioxhost},
            {"Italic",italic},
            {"Joomla",joomla},
            {"Jpy",jpy},
            {"Jsfiddle",jsfiddle},
            {"Key",key},
            {"Keyboard O",keyboard_o},
            {"Krw",krw},
            {"Language",language},
            {"Laptop",laptop},
            {"Lastfm",lastfm},
            {"Lastfm Square",lastfm_square},
            {"Leaf",leaf},
            {"Leanpub",leanpub},
            {"Legal Alias",legal_alias},
            {"Lemon O",lemon_o},
            {"Level Down",level_down},
            {"Level Up",level_up},
            {"Life Bouy Alias",life_bouy_alias},
            {"Life Buoy Alias",life_buoy_alias},
            {"Life Ring",life_ring},
            {"Life Saver Alias",life_saver_alias},
            {"Lightbulb O",lightbulb_o},
            {"Line Chart",line_chart},
            {"Link",link},
            {"Linkedin",linkedin},
            {"Linkedin Square",linkedin_square},
            {"Linode",linode},
            {"Linux",linux},
            {"List",list},
            {"List Alt",list_alt},
            {"List Ol",list_ol},
            {"List Ul",list_ul},
            {"Location Arrow",location_arrow},
            {"Lock",lock_},
            {"Long Arrow Down",long_arrow_down},
            {"Long Arrow Left",long_arrow_left},
            {"Long Arrow Right",long_arrow_right},
            {"Long Arrow Up",long_arrow_up},
            {"Low Vision",low_vision},
            {"Magic",magic},
            {"Magnet",magnet},
            {"Mail Forward Alias",mail_forward_alias},
            {"Mail Reply Alias",mail_reply_alias},
            {"Mail Reply All Alias",mail_reply_all_alias},
            {"Male",male},
            {"Map",map},
            {"Map Marker",map_marker},
            {"Map O",map_o},
            {"Map Pin",map_pin},
            {"Map Signs",map_signs},
            {"Mars",mars},
            {"Mars Double",mars_double},
            {"Mars Stroke",mars_stroke},
            {"Mars Stroke H",mars_stroke_h},
            {"Mars Stroke V",mars_stroke_v},
            {"Maxcdn",maxcdn},
            {"Meanpath",meanpath},
            {"Medium",medium},
            {"Medkit",medkit},
            {"Meetup",meetup},
            {"Meh O",meh_o},
            {"Mercury",mercury},
            {"Microchip",microchip},
            {"Microphone",microphone},
            {"Microphone Slash",microphone_slash},
            {"Minus",minus},
            {"Minus Circle",minus_circle},
            {"Minus Square",minus_square},
            {"Minus Square O",minus_square_o},
            {"Mixcloud",mixcloud},
            {"Mobile",mobile},
            {"Mobile Phone Alias",mobile_phone_alias},
            {"Modx",modx},
            {"Money",money},
            {"Moon O",moon_o},
            {"Mortar Board Alias",mortar_board_alias},
            {"Motorcycle",motorcycle},
            {"Mouse Pointer",mouse_pointer},
            {"Music",music},
            {"Navicon Alias",navicon_alias},
            {"Neuter",neuter},
            {"Newspaper O",newspaper_o},
            {"Object Group",object_group},
            {"Object Ungroup",object_ungroup},
            {"Odnoklassniki",odnoklassniki},
            {"Odnoklassniki Square",odnoklassniki_square},
            {"Opencart",opencart},
            {"Openid",openid},
            {"Opera",opera},
            {"Optin Monster",optin_monster},
            {"Outdent",outdent},
            {"Pagelines",pagelines},
            {"Paint Brush",paint_brush},
            {"Paper Plane",paper_plane},
            {"Paper Plane O",paper_plane_o},
            {"Paperclip",paperclip},
            {"Paragraph",paragraph},
            {"Paste Alias",paste_alias},
            {"Pause",pause},
            {"Pause Circle",pause_circle},
            {"Pause Circle O",pause_circle_o},
            {"Paw",paw},
            {"Paypal",paypal},
            {"Pencil",pencil},
            {"Pencil Square",pencil_square},
            {"Pencil Square O",pencil_square_o},
            {"Percent",percent},
            {"Phone",phone},
            {"Phone Square",phone_square},
            {"Photo Alias",photo_alias},
            {"Picture O",picture_o},
            {"Pie Chart",pie_chart},
            {"Pied Piper",pied_piper},
            {"Pied Piper Alt",pied_piper_alt},
            {"Pied Piper Pp",pied_piper_pp},
            {"Pinterest",pinterest},
            {"Pinterest P",pinterest_p},
            {"Pinterest Square",pinterest_square},
            {"Plane",plane},
            {"Play",play},
            {"Play Circle",play_circle},
            {"Play Circle O",play_circle_o},
            {"Plug",plug},
            {"Plus",plus},
            {"Plus Circle",plus_circle},
            {"Plus Square",plus_square},
            {"Plus Square O",plus_square_o},
            {"Podcast",podcast},
            {"Power Off",power_off},
            {"Print",print},
            {"Product Hunt",product_hunt},
            {"Puzzle Piece",puzzle_piece},
            {"Qq",qq},
            {"Qrcode",qrcode},
            {"Question",question},
            {"Question Circle",question_circle},
            {"Question Circle O",question_circle_o},
            {"Quora",quora},
            {"Quote Left",quote_left},
            {"Quote Right",quote_right},
            {"Ra Alias",ra_alias},
            {"Random",random},
            {"Ravelry",ravelry},
            {"Rebel",rebel},
            {"Recycle",recycle},
            {"Reddit",reddit},
            {"Reddit Alien",reddit_alien},
            {"Reddit Square",reddit_square},
            {"Refresh",refresh},
            {"Registered",registered},
            {"Remove Alias",remove_alias},
            {"Renren",renren},
            {"Reorder Alias",reorder_alias},
            {"Repeat",repeat},
            {"Reply",reply},
            {"Reply All",reply_all},
            {"Resistance Alias",resistance_alias},
            {"Retweet",retweet},
            {"Rmb Alias",rmb_alias},
            {"Road",road},
            {"Rocket",rocket},
            {"Rotate Left Alias",rotate_left_alias},
            {"Rotate Right Alias",rotate_right_alias},
            {"Rouble Alias",rouble_alias},
            {"Rss",rss},
            {"Rss Square",rss_square},
            {"Rub",rub},
            {"Ruble Alias",ruble_alias},
            {"Rupee Alias",rupee_alias},
            {"S15 Alias",s15_alias},
            {"Safari",safari},
            {"Save Alias",save_alias},
            {"Scissors",scissors},
            {"Scribd",scribd},
            {"Search",search},
            {"Search Minus",search_minus},
            {"Search Plus",search_plus},
            {"Sellsy",sellsy},
            {"Send Alias",send_alias},
            {"Send O Alias",send_o_alias},
            {"Server",server},
            {"Share",share},
            {"Share Alt",share_alt},
            {"Share Alt Square",share_alt_square},
            {"Share Square",share_square},
            {"Share Square O",share_square_o},
            {"Shekel Alias",shekel_alias},
            {"Sheqel Alias",sheqel_alias},
            {"Shield",shield},
            {"Ship",ship},
            {"Shirtsinbulk",shirtsinbulk},
            {"Shopping Bag",shopping_bag},
            {"Shopping Basket",shopping_basket},
            {"Shopping Cart",shopping_cart},
            {"Shower",shower},
            {"Sign In",sign_in},
            {"Sign Language",sign_language},
            {"Sign Out",sign_out},
            {"Signal",signal},
            {"Signing Alias",signing_alias},
            {"Simplybuilt",simplybuilt},
            {"Sitemap",sitemap},
            {"Skyatlas",skyatlas},
            {"Skype",skype},
            {"Slack",slack},
            {"Sliders",sliders},
            {"Slideshare",slideshare},
            {"Smile O",smile_o},
            {"Snapchat",snapchat},
            {"Snapchat Ghost",snapchat_ghost},
            {"Snapchat Square",snapchat_square},
            {"Snowflake O",snowflake_o},
            {"Soccer Ball O Alias",soccer_ball_o_alias},
            {"Sort",sort},
            {"Sort Alpha Asc",sort_alpha_asc},
            {"Sort Alpha Desc",sort_alpha_desc},
            {"Sort Amount Asc",sort_amount_asc},
            {"Sort Amount Desc",sort_amount_desc},
            {"Sort Asc",sort_asc},
            {"Sort Desc",sort_desc},
            {"Sort Down Alias",sort_down_alias},
            {"Sort Numeric Asc",sort_numeric_asc},
            {"Sort Numeric Desc",sort_numeric_desc},
            {"Sort Up Alias",sort_up_alias},
            {"Soundcloud",soundcloud},
            {"Space Shuttle",space_shuttle},
            {"Spinner",spinner},
            {"Spoon",spoon},
            {"Spotify",spotify},
            {"Square",square},
            {"Square O",square_o},
            {"Stack Exchange",stack_exchange},
            {"Stack Overflow",stack_overflow},
            {"Star",star},
            {"Star Half",star_half},
            {"Star Half Empty Alias",star_half_empty_alias},
            {"Star Half Full Alias",star_half_full_alias},
            {"Star Half O",star_half_o},
            {"Star O",star_o},
            {"Steam",steam},
            {"Steam Square",steam_square},
            {"Step Backward",step_backward},
            {"Step Forward",step_forward},
            {"Stethoscope",stethoscope},
            {"Sticky Note",sticky_note},
            {"Sticky Note O",sticky_note_o},
            {"Stop",stop},
            {"Stop Circle",stop_circle},
            {"Stop Circle O",stop_circle_o},
            {"Street View",street_view},
            {"Strikethrough",strikethrough},
            {"Stumbleupon",stumbleupon},
            {"Stumbleupon Circle",stumbleupon_circle},
            {"Subscript",subscript},
            {"Subway",subway},
            {"Suitcase",suitcase},
            {"Sun O",sun_o},
            {"Superpowers",superpowers},
            {"Superscript",superscript},
            {"Support Alias",support_alias},
            {"Table",table},
            {"Tablet",tablet},
            {"Tachometer",tachometer},
            {"Tag",tag},
            {"Tags",tags},
            {"Tasks",tasks},
            {"Taxi",taxi},
            {"Telegram",telegram},
            {"Television",television},
            {"Tencent Weibo",tencent_weibo},
            {"Terminal",terminal},
            {"Text Height",text_height},
            {"Text Width",text_width},
            {"Th",th},
            {"Th Large",th_large},
            {"Th List",th_list},
            {"Themeisle",themeisle},
            {"Thermometer Alias",thermometer_alias},
            {"Thermometer 0 Alias",thermometer_0_alias},
            {"Thermometer 1 Alias",thermometer_1_alias},
            {"Thermometer 2 Alias",thermometer_2_alias},
            {"Thermometer 3 Alias",thermometer_3_alias},
            {"Thermometer 4 Alias",thermometer_4_alias},
            {"Thermometer Empty",thermometer_empty},
            {"Thermometer Full",thermometer_full},
            {"Thermometer Half",thermometer_half},
            {"Thermometer Quarter",thermometer_quarter},
            {"Thermometer Three Quarters",thermometer_three_quarters},
            {"Thumb Tack",thumb_tack},
            {"Thumbs Down",thumbs_down},
            {"Thumbs O Down",thumbs_o_down},
            {"Thumbs O Up",thumbs_o_up},
            {"Thumbs Up",thumbs_up},
            {"Ticket",ticket},
            {"Times",times},
            {"Times Circle",times_circle},
            {"Times Circle O",times_circle_o},
            {"Times Rectangle Alias",times_rectangle_alias},
            {"Times Rectangle O Alias",times_rectangle_o_alias},
            {"Tint",tint},
            {"Toggle Down Alias",toggle_down_alias},
            {"Toggle Left Alias",toggle_left_alias},
            {"Toggle Off",toggle_off},
            {"Toggle On",toggle_on},
            {"Toggle Right Alias",toggle_right_alias},
            {"Toggle Up Alias",toggle_up_alias},
            {"Trademark",trademark},
            {"Train",train},
            {"Transgender",transgender},
            {"Transgender Alt",transgender_alt},
            {"Trash",trash},
            {"Trash O",trash_o},
            {"Tree",tree},
            {"Trello",trello},
            {"Tripadvisor",tripadvisor},
            {"Trophy",trophy},
            {"Truck",truck},
            {"Try",try_},
            {"Tty",tty},
            {"Tumblr",tumblr},
            {"Tumblr Square",tumblr_square},
            {"Turkish Lira Alias",turkish_lira_alias},
            {"Tv Alias",tv_alias},
            {"Twitch",twitch},
            {"Twitter",twitter},
            {"Twitter Square",twitter_square},
            {"Umbrella",umbrella},
            {"Underline",underline},
            {"Undo",undo},
            {"Universal Access",universal_access},
            {"University",university},
            {"Unlink Alias",unlink_alias},
            {"Unlock",unlock},
            {"Unlock Alt",unlock_alt},
            {"Unsorted Alias",unsorted_alias},
            {"Upload",upload},
            {"Usb",usb},
            {"Usd",usd},
            {"User",user},
            {"User Circle",user_circle},
            {"User Circle O",user_circle_o},
            {"User Md",user_md},
            {"User O",user_o},
            {"User Plus",user_plus},
            {"User Secret",user_secret},
            {"User Times",user_times},
            {"Users",users},
            {"Vcard Alias",vcard_alias},
            {"Vcard O Alias",vcard_o_alias},
            {"Venus",venus},
            {"Venus Double",venus_double},
            {"Venus Mars",venus_mars},
            {"Viacoin",viacoin},
            {"Viadeo",viadeo},
            {"Viadeo Square",viadeo_square},
            {"Video Camera",video_camera},
            {"Vimeo",vimeo},
            {"Vimeo Square",vimeo_square},
            {"Vine",vine},
            {"Vk",vk},
            {"Volume Control Phone",volume_control_phone},
            {"Volume Down",volume_down},
            {"Volume Off",volume_off},
            {"Volume Up",volume_up},
            {"Warning Alias",warning_alias},
            {"Wechat Alias",wechat_alias},
            {"Weibo",weibo},
            {"Weixin",weixin},
            {"Whatsapp",whatsapp},
            {"Wheelchair",wheelchair},
            {"Wheelchair Alt",wheelchair_alt},
            {"Wifi",wifi},
            {"Wikipedia W",wikipedia_w},
            {"Window Close",window_close},
            {"Window Close O",window_close_o},
            {"Window Maximize",window_maximize},
            {"Window Minimize",window_minimize},
            {"Window Restore",window_restore},
            {"Windows",windows},
            {"Won Alias",won_alias},
            {"Wordpress",wordpress},
            {"Wpbeginner",wpbeginner},
            {"Wpexplorer",wpexplorer},
            {"Wpforms",wpforms},
            {"Wrench",wrench},
            {"Xing",xing},
            {"Xing Square",xing_square},
            {"Y Combinator",y_combinator},
            {"Y Combinator Square Alias",y_combinator_square_alias},
            {"Yahoo",yahoo},
            {"Yc Alias",yc_alias},
            {"Yc Square Alias",yc_square_alias},
            {"Yelp",yelp},
            {"Yen Alias",yen_alias},
            {"Yoast",yoast},
            {"Youtube",youtube},
            {"Youtube Play",youtube_play},
            {"Youtube Square",youtube_square},

        };
        public static Dictionary<string, string> WebApplicationIcons = new Dictionary<string, string>()
        {
            {"Address Book",address_book},
            {"Address Book O",address_book_o},
            {"Address Card",address_card},
            {"Address Card O",address_card_o},
            {"Adjust",adjust},
            {"American Sign Language Interpreting",american_sign_language_interpreting},
            {"Anchor",anchor},
            {"Archive",archive},
            {"Area Chart",area_chart},
            {"Arrows",arrows},
            {"Arrows H",arrows_h},
            {"Arrows V",arrows_v},
            {"Asl Interpreting Alias",asl_interpreting_alias},
            {"Assistive Listening Systems",assistive_listening_systems},
            {"Asterisk",asterisk},
            {"At",at},
            {"Audio Description",audio_description},
            {"Automobile Alias",automobile_alias},
            {"Balance Scale",balance_scale},
            {"Ban",ban},
            {"Bank Alias",bank_alias},
            {"Bar Chart",bar_chart},
            {"Bar Chart O Alias",bar_chart_o_alias},
            {"Barcode",barcode},
            {"Bars",bars},
            {"Bath",bath},
            {"Bathtub Alias",bathtub_alias},
            {"Battery Alias",battery_alias},
            {"Battery 0 Alias",battery_0_alias},
            {"Battery 1 Alias",battery_1_alias},
            {"Battery 2 Alias",battery_2_alias},
            {"Battery 3 Alias",battery_3_alias},
            {"Battery 4 Alias",battery_4_alias},
            {"Battery Empty",battery_empty},
            {"Battery Full",battery_full},
            {"Battery Half",battery_half},
            {"Battery Quarter",battery_quarter},
            {"Battery Three Quarters",battery_three_quarters},
            {"Bed",bed},
            {"Beer",beer},
            {"Bell",bell},
            {"Bell O",bell_o},
            {"Bell Slash",bell_slash},
            {"Bell Slash O",bell_slash_o},
            {"Bicycle",bicycle},
            {"Binoculars",binoculars},
            {"Birthday Cake",birthday_cake},
            {"Blind",blind},
            {"Bluetooth",bluetooth},
            {"Bluetooth B",bluetooth_b},
            {"Bolt",bolt},
            {"Bomb",bomb},
            {"Book",book},
            {"Bookmark",bookmark},
            {"Bookmark O",bookmark_o},
            {"Braille",braille},
            {"Briefcase",briefcase},
            {"Bug",bug},
            {"Building",building},
            {"Building O",building_o},
            {"Bullhorn",bullhorn},
            {"Bullseye",bullseye},
            {"Bus",bus},
            {"Cab Alias",cab_alias},
            {"Calculator",calculator},
            {"Calendar",calendar},
            {"Calendar Check O",calendar_check_o},
            {"Calendar Minus O",calendar_minus_o},
            {"Calendar O",calendar_o},
            {"Calendar Plus O",calendar_plus_o},
            {"Calendar Times O",calendar_times_o},
            {"Camera",camera},
            {"Camera Retro",camera_retro},
            {"Car",car},
            {"Caret Square O Down",caret_square_o_down},
            {"Caret Square O Left",caret_square_o_left},
            {"Caret Square O Right",caret_square_o_right},
            {"Caret Square O Up",caret_square_o_up},
            {"Cart Arrow Down",cart_arrow_down},
            {"Cart Plus",cart_plus},
            {"Cc",cc},
            {"Certificate",certificate},
            {"Check",check},
            {"Check Circle",check_circle},
            {"Check Circle O",check_circle_o},
            {"Check Square",check_square},
            {"Check Square O",check_square_o},
            {"Child",child},
            {"Circle",circle},
            {"Circle O",circle_o},
            {"Circle O Notch",circle_o_notch},
            {"Circle Thin",circle_thin},
            {"Clock O",clock_o},
            {"Clone",clone},
            {"Close Alias",close_alias},
            {"Cloud",cloud},
            {"Cloud Download",cloud_download},
            {"Cloud Upload",cloud_upload},
            {"Code",code},
            {"Code Fork",code_fork},
            {"Coffee",coffee},
            {"Cog",cog},
            {"Cogs",cogs},
            {"Comment",comment},
            {"Comment O",comment_o},
            {"Commenting",commenting},
            {"Commenting O",commenting_o},
            {"Comments",comments},
            {"Comments O",comments_o},
            {"Compass",compass},
            {"Copyright",copyright},
            {"Creative Commons",creative_commons},
            {"Credit Card",credit_card},
            {"Credit Card Alt",credit_card_alt},
            {"Crop",crop},
            {"Crosshairs",crosshairs},
            {"Cube",cube},
            {"Cubes",cubes},
            {"Cutlery",cutlery},
            {"Dashboard Alias",dashboard_alias},
            {"Database",database},
            {"Deaf",deaf},
            {"Deafness Alias",deafness_alias},
            {"Desktop",desktop},
            {"Diamond",diamond},
            {"Dot Circle O",dot_circle_o},
            {"Download",download},
            {"Drivers License Alias",drivers_license_alias},
            {"Drivers License O Alias",drivers_license_o_alias},
            {"Edit Alias",edit_alias},
            {"Ellipsis H",ellipsis_h},
            {"Ellipsis V",ellipsis_v},
            {"Envelope",envelope},
            {"Envelope O",envelope_o},
            {"Envelope Open",envelope_open},
            {"Envelope Open O",envelope_open_o},
            {"Envelope Square",envelope_square},
            {"Eraser",eraser},
            {"Exchange",exchange},
            {"Exclamation",exclamation},
            {"Exclamation Circle",exclamation_circle},
            {"Exclamation Triangle",exclamation_triangle},
            {"External Link",external_link},
            {"External Link Square",external_link_square},
            {"Eye",eye},
            {"Eye Slash",eye_slash},
            {"Eyedropper",eyedropper},
            {"Fax",fax},
            {"Feed Alias",feed_alias},
            {"Female",female},
            {"Fighter Jet",fighter_jet},
            {"File",file},
            {"File Archive O",file_archive_o},
            {"File Audio O",file_audio_o},
            {"File Code O",file_code_o},
            {"File Excel O",file_excel_o},
            {"File Image O",file_image_o},
            {"File Movie O Alias",file_movie_o_alias},
            {"File O",file_o},
            {"File Pdf O",file_pdf_o},
            {"File Photo O Alias",file_photo_o_alias},
            {"File Picture O Alias",file_picture_o_alias},
            {"File Powerpoint O",file_powerpoint_o},
            {"File Sound O Alias",file_sound_o_alias},
            {"File Text",file_text},
            {"File Text O",file_text_o},
            {"File Video O",file_video_o},
            {"File Word O",file_word_o},
            {"File Zip O Alias",file_zip_o_alias},
            {"Files O",files_o},
            {"Film",film},
            {"Filter",filter},
            {"Fire",fire},
            {"Fire Extinguisher",fire_extinguisher},
            {"Flag",flag},
            {"Flag Checkered",flag_checkered},
            {"Flag O",flag_o},
            {"Flash Alias",flash_alias},
            {"Flask",flask},
            {"Folder",folder},
            {"Folder O",folder_o},
            {"Folder Open",folder_open},
            {"Folder Open O",folder_open_o},
            {"Frown O",frown_o},
            {"Futbol O",futbol_o},
            {"Gamepad",gamepad},
            {"Gavel",gavel},
            {"Gear Alias",gear_alias},
            {"Gears Alias",gears_alias},
            {"Gift",gift},
            {"Glass",glass},
            {"Globe",globe},
            {"Graduation Cap",graduation_cap},
            {"Group Alias",group_alias},
            {"Hand Grab O Alias",hand_grab_o_alias},
            {"Hand Lizard O",hand_lizard_o},
            {"Hand O Down",hand_o_down},
            {"Hand O Left",hand_o_left},
            {"Hand O Right",hand_o_right},
            {"Hand O Up",hand_o_up},
            {"Hand Paper O",hand_paper_o},
            {"Hand Peace O",hand_peace_o},
            {"Hand Pointer O",hand_pointer_o},
            {"Hand Rock O",hand_rock_o},
            {"Hand Scissors O",hand_scissors_o},
            {"Hand Spock O",hand_spock_o},
            {"Hand Stop O Alias",hand_stop_o_alias},
            {"Handshake O",handshake_o},
            {"Hard Of Hearing Alias",hard_of_hearing_alias},
            {"Hashtag",hashtag},
            {"Hdd O",hdd_o},
            {"Headphones",headphones},
            {"Heart",heart},
            {"Heart O",heart_o},
            {"Heartbeat",heartbeat},
            {"History",history},
            {"Home",home},
            {"Hotel Alias",hotel_alias},
            {"Hourglass",hourglass},
            {"Hourglass 1 Alias",hourglass_1_alias},
            {"Hourglass 2 Alias",hourglass_2_alias},
            {"Hourglass 3 Alias",hourglass_3_alias},
            {"Hourglass End",hourglass_end},
            {"Hourglass Half",hourglass_half},
            {"Hourglass O",hourglass_o},
            {"Hourglass Start",hourglass_start},
            {"I Cursor",i_cursor},
            {"Id Badge",id_badge},
            {"Id Card",id_card},
            {"Id Card O",id_card_o},
            {"Image Alias",image_alias},
            {"Inbox",inbox},
            {"Industry",industry},
            {"Info",info},
            {"Info Circle",info_circle},
            {"Institution Alias",institution_alias},
            {"Key",key},
            {"Keyboard O",keyboard_o},
            {"Language",language},
            {"Laptop",laptop},
            {"Leaf",leaf},
            {"Legal Alias",legal_alias},
            {"Lemon O",lemon_o},
            {"Level Down",level_down},
            {"Level Up",level_up},
            {"Life Bouy Alias",life_bouy_alias},
            {"Life Buoy Alias",life_buoy_alias},
            {"Life Ring",life_ring},
            {"Life Saver Alias",life_saver_alias},
            {"Lightbulb O",lightbulb_o},
            {"Line Chart",line_chart},
            {"Location Arrow",location_arrow},
            {"Lock",lock_},
            {"Low Vision",low_vision},
            {"Magic",magic},
            {"Magnet",magnet},
            {"Mail Forward Alias",mail_forward_alias},
            {"Mail Reply Alias",mail_reply_alias},
            {"Mail Reply All Alias",mail_reply_all_alias},
            {"Male",male},
            {"Map",map},
            {"Map Marker",map_marker},
            {"Map O",map_o},
            {"Map Pin",map_pin},
            {"Map Signs",map_signs},
            {"Meh O",meh_o}, 
            {"Microchip",microchip},
            {"Microphone",microphone},
            {"Microphone Slash",microphone_slash},
            {"Minus",minus},
            {"Minus Circle",minus_circle},
            {"Minus Square",minus_square},
            {"Minus Square O",minus_square_o},
            {"Mobile",mobile},
            {"Mobile Phone Alias",mobile_phone_alias},
            {"Money",money},
            {"Moon O",moon_o},
            {"Mortar Board Alias",mortar_board_alias},
            {"Motorcycle",motorcycle},
            {"Mouse Pointer",mouse_pointer},
            {"Music",music},
            {"Navicon Alias",navicon_alias},
            {"Newspaper O",newspaper_o},
            {"Object Group",object_group},
            {"Object Ungroup",object_ungroup},
            {"Paint Brush",paint_brush},
            {"Paper Plane",paper_plane},
            {"Paper Plane O",paper_plane_o},
            {"Paw",paw},
            {"Pencil",pencil},
            {"Pencil Square",pencil_square},
            {"Pencil Square O",pencil_square_o},
            {"Percent",percent},
            {"Phone",phone},
            {"Phone Square",phone_square},
            {"Photo Alias",photo_alias},
            {"Picture O",picture_o},
            {"Pie Chart",pie_chart},
            {"Plane",plane},
            {"Plug",plug},
            {"Plus",plus},
            {"Plus Circle",plus_circle},
            {"Plus Square",plus_square},
            {"Plus Square O",plus_square_o},
            {"Podcast",podcast},
            {"Power Off",power_off},
            {"Print",print},
            {"Puzzle Piece",puzzle_piece},
            {"Qrcode",qrcode},
            {"Question",question},
            {"Question Circle",question_circle},
            {"Question Circle O",question_circle_o},
            {"Quote Left",quote_left},
            {"Quote Right",quote_right},
            {"Random",random},
            {"Recycle",recycle},
            {"Refresh",refresh},
            {"Registered",registered},
            {"Remove Alias",remove_alias},
            {"Reorder Alias",reorder_alias},
            {"Reply",reply},
            {"Reply All",reply_all},
            {"Retweet",retweet},
            {"Road",road},
            {"Rocket",rocket},
            {"Rss",rss},
            {"Rss Square",rss_square},
            {"S15 Alias",s15_alias},
            {"Search",search},
            {"Search Minus",search_minus},
            {"Search Plus",search_plus},
            {"Send Alias",send_alias},
            {"Send O Alias",send_o_alias},
            {"Server",server},
            {"Share",share},
            {"Share Alt",share_alt},
            {"Share Alt Square",share_alt_square},
            {"Share Square",share_square},
            {"Share Square O",share_square_o},
            {"Shield",shield},
            {"Ship",ship},
            {"Shopping Bag",shopping_bag},
            {"Shopping Basket",shopping_basket},
            {"Shopping Cart",shopping_cart},
            {"Shower",shower},
            {"Sign In",sign_in},
            {"Sign Language",sign_language},
            {"Sign Out",sign_out},
            {"Signal",signal},
            {"Signing Alias",signing_alias},
            {"Sitemap",sitemap},
            {"Sliders",sliders},
            {"Smile O",smile_o},
            {"Snowflake O",snowflake_o},
            {"Soccer Ball O Alias",soccer_ball_o_alias},
            {"Sort",sort},
            {"Sort Alpha Asc",sort_alpha_asc},
            {"Sort Alpha Desc",sort_alpha_desc},
            {"Sort Amount Asc",sort_amount_asc},
            {"Sort Amount Desc",sort_amount_desc},
            {"Sort Asc",sort_asc},
            {"Sort Desc",sort_desc},
            {"Sort Down Alias",sort_down_alias},
            {"Sort Numeric Asc",sort_numeric_asc},
            {"Sort Numeric Desc",sort_numeric_desc},
            {"Sort Up Alias",sort_up_alias},
            {"Space Shuttle",space_shuttle},
            {"Spinner",spinner},
            {"Spoon",spoon},
            {"Square",square},
            {"Square O",square_o},
            {"Star",star},
            {"Star Half",star_half},
            {"Star Half Empty Alias",star_half_empty_alias},
            {"Star Half Full Alias",star_half_full_alias},
            {"Star Half O",star_half_o},
            {"Star O",star_o},
            {"Sticky Note",sticky_note},
            {"Sticky Note O",sticky_note_o},
            {"Street View",street_view},
            {"Suitcase",suitcase},
            {"Sun O",sun_o},
            {"Support Alias",support_alias},
            {"Tablet",tablet},
            {"Tachometer",tachometer},
            {"Tag",tag},
            {"Tags",tags},
            {"Tasks",tasks},
            {"Taxi",taxi},
            {"Television",television},
            {"Terminal",terminal},
            {"Thermometer Alias",thermometer_alias},
            {"Thermometer 0 Alias",thermometer_0_alias},
            {"Thermometer 1 Alias",thermometer_1_alias},
            {"Thermometer 2 Alias",thermometer_2_alias},
            {"Thermometer 3 Alias",thermometer_3_alias},
            {"Thermometer 4 Alias",thermometer_4_alias},
            {"Thermometer Empty",thermometer_empty},
            {"Thermometer Full",thermometer_full},
            {"Thermometer Half",thermometer_half},
            {"Thermometer Quarter",thermometer_quarter},
            {"Thermometer Three Quarters",thermometer_three_quarters},
            {"Thumb Tack",thumb_tack},
            {"Thumbs Down",thumbs_down},
            {"Thumbs O Down",thumbs_o_down},
            {"Thumbs O Up",thumbs_o_up},
            {"Thumbs Up",thumbs_up},
            {"Ticket",ticket},
            {"Times",times},
            {"Times Circle",times_circle},
            {"Times Circle O",times_circle_o},
            {"Times Rectangle Alias",times_rectangle_alias},
            {"Times Rectangle O Alias",times_rectangle_o_alias},
            {"Tint",tint},
            {"Toggle Down Alias",toggle_down_alias},
            {"Toggle Left Alias",toggle_left_alias},
            {"Toggle Off",toggle_off},
            {"Toggle On",toggle_on},
            {"Toggle Right Alias",toggle_right_alias},
            {"Toggle Up Alias",toggle_up_alias},
            {"Trademark",trademark},
            {"Trash",trash},
            {"Trash O",trash_o},
            {"Tree",tree},
            {"Trophy",trophy},
            {"Truck",truck},
            {"Try",try_},
            {"Tv Alias",tv_alias},
            {"Umbrella",umbrella},
            {"Universal Access",universal_access},
            {"University",university},
            {"Unlock",unlock},
            {"Unlock Alt",unlock_alt},
            {"Unsorted Alias",unsorted_alias},
            {"Upload",upload},
            {"User",user},
            {"User Circle",user_circle},
            {"User Circle O",user_circle_o},
            {"User O",user_o},
            {"User Plus",user_plus},
            {"User Secret",user_secret},
            {"User Times",user_times},
            {"Users",users},
            {"Vcard Alias",vcard_alias},
            {"Vcard O Alias",vcard_o_alias},
            {"Video Camera",video_camera},
            {"Volume Control Phone",volume_control_phone},
            {"Volume Down",volume_down},
            {"Volume Off",volume_off},
            {"Volume Up",volume_up},
            {"Warning Alias",warning_alias},
            {"Wheelchair",wheelchair},
            {"Wheelchair Alt",wheelchair_alt},
            {"Wifi",wifi},
            {"Window Close",window_close},
            {"Window Close O",window_close_o},
            {"Window Maximize",window_maximize},
            {"Window Minimize",window_minimize},
            {"Window Restore",window_restore},
            {"Wrench",wrench},
        };
        public static Dictionary<string, string> AccessibilityIcons = new Dictionary<string, string>()
        {
            {"American Sign Language Interpreting",american_sign_language_interpreting},
            {"Asl Interpreting Alias",asl_interpreting_alias},
            {"Assistive Listening Systems",assistive_listening_systems},
            {"Audio Description",audio_description},
            {"Blind",blind},
            {"Braille",braille},
            {"Cc",cc},
            {"Deaf",deaf},
            {"Deafness Alias",deafness_alias},
            {"Hard Of Hearing Alias",hard_of_hearing_alias},
            {"Low Vision",low_vision},
            {"Question Circle",question_circle},
            {"Sign Language",sign_language},
            {"Signing Alias",signing_alias},
            {"Tty",tty},
            {"Universal Access",universal_access},
            {"Volume Control Phone",volume_control_phone},
            {"Wheelchair",wheelchair},
            {"Wheelchair Alt",wheelchair_alt},
        };
        public static Dictionary<string, string> HandIcons = new Dictionary<string, string>()
        {
            {"Hand Grab O Alias",hand_grab_o_alias},
            {"Hand Lizard O",hand_lizard_o},
            {"Hand O Down",hand_o_down},
            {"Hand O Left",hand_o_left},
            {"Hand O Right",hand_o_right},
            {"Hand O Up",hand_o_up},
            {"Hand Paper O",hand_paper_o},
            {"Hand Peace O",hand_peace_o},
            {"Hand Pointer O",hand_pointer_o},
            {"Hand Rock O",hand_rock_o},
            {"Hand Scissors O",hand_scissors_o},
            {"Hand Spock O",hand_spock_o},
            {"Hand Stop O Alias",hand_stop_o_alias},
            {"Thumbs Down",thumbs_down},
            {"Thumbs O Down",thumbs_o_down},
            {"Thumbs O Up",thumbs_o_up},
            {"Thumbs Up",thumbs_up},
        };
        public static Dictionary<string, string> TransportationIcons = new Dictionary<string, string>()
        {
            {"Ambulance",ambulance},
            {"Automobile Alias",automobile_alias},
            {"Bicycle",bicycle},
            {"Bus",bus},
            {"Cab Alias",cab_alias},
            {"Car",car},
            {"Fighter Jet",fighter_jet},
            {"Motorcycle",motorcycle},
            {"Plane",plane},
            {"Rocket",rocket},
            {"Ship",ship},
            {"Train",train},
            {"Truck",truck},
            {"Wheelchair",wheelchair},
            {"Wheelchair Alt",wheelchair_alt},
        };
        public static Dictionary<string, string> GenderIcons = new Dictionary<string, string>()
        {
            {"Genderless",genderless},
            {"Intersex Alias",intersex_alias},
            {"Mars",mars},
            {"Mars Double",mars_double},
            {"Mars Stroke",mars_stroke},
            {"Mars Stroke H",mars_stroke_h},
            {"Mars Stroke V",mars_stroke_v},
            {"Mercury",mercury},
            {"Neuter",neuter},
            {"Transgender",transgender},
            {"Transgender Alt",transgender_alt},
            {"Venus",venus},
            {"Venus Double",venus_double},
            {"Venus Mars",venus_mars},
        };
        public static Dictionary<string, string> FileTypeIcons = new Dictionary<string, string>()
        {
            {"File",file},
            {"File Archive O",file_archive_o},
            {"File Audio O",file_audio_o},
            {"File Code O",file_code_o},
            {"File Excel O",file_excel_o},
            {"File Image O",file_image_o},
            {"File Movie O Alias",file_movie_o_alias},
            {"File O",file_o},
            {"File Pdf O",file_pdf_o},
            {"File Photo O Alias",file_photo_o_alias},
            {"File Picture O Alias",file_picture_o_alias},
            {"File Powerpoint O",file_powerpoint_o},
            {"File Sound O Alias",file_sound_o_alias},
            {"File Text",file_text},
            {"File Text O",file_text_o},
            {"File Video O",file_video_o},
            {"File Word O",file_word_o},
            {"File Zip O Alias",file_zip_o_alias},
        };
        public static Dictionary<string, string> SpinnerIcons = new Dictionary<string, string>()
        {
            {"Circle O Notch",circle_o_notch},
            {"Cog",cog},
            {"Gear Alias",gear_alias},
            {"Gears Alias",gears_alias},
            {"Refresh",refresh},
            {"Spinner",spinner},
        };
        public static Dictionary<string, string> FormControlIcons = new Dictionary<string, string>()
        {
            {"Check Square",check_square},
            {"Check Square O",check_square_o},
            {"Circle",circle},
            {"Circle O",circle_o},
            {"Circle O Notch",circle_o_notch},
            {"Dot Circle O",dot_circle_o},
            {"Minus Square",minus_square},
            {"Minus Square O",minus_square_o},
            {"Plus Square",plus_square},
            {"Plus Square O",plus_square_o},
            {"Square",square},
            {"Square O",square_o},
        };
        public static Dictionary<string, string> PaymentIcons = new Dictionary<string, string>()
        {
            {"Cc Amex",cc_amex},
            {"Cc Diners Club",cc_diners_club},
            {"Cc Discover",cc_discover},
            {"Cc Jcb",cc_jcb},
            {"Cc Mastercard",cc_mastercard},
            {"Cc Paypal",cc_paypal},
            {"Cc Stripe",cc_stripe},
            {"Cc Visa",cc_visa},
            {"Credit Card",credit_card},
            {"Credit Card Alt",credit_card_alt},
            {"Google Wallet",google_wallet},
            {"Paypal",paypal},
        };
        public static Dictionary<string, string> ChartIcons = new Dictionary<string, string>()
        {
            {"Area Chart",area_chart},
            {"Bar Chart",bar_chart},
            {"Bar Chart O Alias",bar_chart_o_alias},
            {"Line Chart",line_chart},
            {"Pie Chart",pie_chart},
        };
        public static Dictionary<string, string> CurrencyIcons = new Dictionary<string, string>()
        {
            {"Bitcoin Alias",bitcoin_alias},
            {"Btc",btc},
            {"Cny Alias",cny_alias},
            {"Dollar Alias",dollar_alias},
            {"Eur",eur},
            {"Euro Alias",euro_alias},
            {"Gbp",gbp},
            {"Gg",gg},
            {"Gg Circle",gg_circle},
            {"Ils",ils},
            {"Inr",inr},
            {"Jpy",jpy},
            {"Krw",krw},
            {"Money",money},
            {"Rmb Alias",rmb_alias},
            {"Rouble Alias",rouble_alias},
            {"Rub",rub},
            {"Ruble Alias",ruble_alias},
            {"Rupee Alias",rupee_alias},
            {"Shekel Alias",shekel_alias},
            {"Sheqel Alias",sheqel_alias},
            {"Try",try_},
            {"Turkish Lira Alias",turkish_lira_alias},
            {"Usd",usd},
            {"Won Alias",won_alias},
            {"Yen Alias",yen_alias},
        };
        public static Dictionary<string, string> TextEditorIcons = new Dictionary<string, string>()
        {
            {"Align Center",align_center},
            {"Align Justify",align_justify},
            {"Align Left",align_left},
            {"Align Right",align_right},
            {"Bold",bold},
            {"Chain Alias",chain_alias},
            {"Chain Broken",chain_broken},
            {"Clipboard",clipboard},
            {"Columns",columns},
            {"Copy Alias",copy_alias},
            {"Cut Alias",cut_alias},
            {"Dedent Alias",dedent_alias},
            {"Eraser",eraser},
            {"File Text",file_text},
            {"File Text O",file_text_o},
            {"Files O",files_o},
            {"Floppy O",floppy_o},
            {"Font",font},
            {"Header",header},
            {"Indent",indent},
            {"Italic",italic},
            {"Link",link},
            {"List",list},
            {"List Alt",list_alt},
            {"List Ol",list_ol},
            {"List Ul",list_ul},
            {"Outdent",outdent},
            {"Pagelines",pagelines},
            {"Paperclip",paperclip},
            {"Paragraph",paragraph},
            {"Paste Alias",paste_alias},
            {"Repeat",repeat},
            {"Rotate Left Alias",rotate_left_alias},
            {"Rotate Right Alias",rotate_right_alias},
            {"Save Alias",save_alias},
            {"Scissors",scissors},
            {"Strikethrough",strikethrough},
            {"Subscript",subscript},
            {"Superscript",superscript},
            {"Table",table},
            {"Text Height",text_height},
            {"Text Width",text_width},
            {"Th",th},
            {"Th Large",th_large},
            {"Th List",th_list},
            {"Underline",underline},
            {"Undo",undo},
        };
        public static Dictionary<string, string> DirectionalIcons = new Dictionary<string, string>()
        {
            {"Angle Double Down",angle_double_down},
            {"Angle Double Left",angle_double_left},
            {"Angle Double Right",angle_double_right},
            {"Angle Double Up",angle_double_up},
            {"Angle Down",angle_down},
            {"Angle Left",angle_left},
            {"Angle Right",angle_right},
            {"Angle Up",angle_up},
            {"Arrow Circle Down",arrow_circle_down},
            {"Arrow Circle Left",arrow_circle_left},
            {"Arrow Circle O Down",arrow_circle_o_down},
            {"Arrow Circle O Left",arrow_circle_o_left},
            {"Arrow Circle O Right",arrow_circle_o_right},
            {"Arrow Circle O Up",arrow_circle_o_up},
            {"Arrow Circle Right",arrow_circle_right},
            {"Arrow Circle Up",arrow_circle_up},
            {"Arrow Down",arrow_down},
            {"Arrow Left",arrow_left},
            {"Arrow Right",arrow_right},
            {"Arrow Up",arrow_up},
            {"Arrows",arrows},
            {"Arrows Alt",arrows_alt},
            {"Arrows H",arrows_h},
            {"Arrows V",arrows_v},
            {"Caret Down",caret_down},
            {"Caret Left",caret_left},
            {"Caret Right",caret_right},
            {"Caret Square O Down",caret_square_o_down},
            {"Caret Square O Left",caret_square_o_left},
            {"Caret Square O Right",caret_square_o_right},
            {"Caret Square O Up",caret_square_o_up},
            {"Caret Up",caret_up},
            {"Chevron Circle Down",chevron_circle_down},
            {"Chevron Circle Left",chevron_circle_left},
            {"Chevron Circle Right",chevron_circle_right},
            {"Chevron Circle Up",chevron_circle_up},
            {"Chevron Down",chevron_down},
            {"Chevron Left",chevron_left},
            {"Chevron Right",chevron_right},
            {"Chevron Up",chevron_up},
            {"Exchange",exchange},
            {"Hand O Down",hand_o_down},
            {"Hand O Left",hand_o_left},
            {"Hand O Right",hand_o_right},
            {"Hand O Up",hand_o_up},
            {"Long Arrow Down",long_arrow_down},
            {"Long Arrow Left",long_arrow_left},
            {"Long Arrow Right",long_arrow_right},
            {"Long Arrow Up",long_arrow_up},
            {"Toggle Down Alias",toggle_down_alias},
            {"Toggle Left Alias",toggle_left_alias},
            {"Toggle Right Alias",toggle_right_alias},
            {"Toggle Up Alias",toggle_up_alias},
        };
        public static Dictionary<string, string> VideoPlayerIcons = new Dictionary<string, string>()
        {
            {"Arrows Alt",arrows_alt},
            {"Backward",backward},
            {"Compress",compress},
            {"Eject",eject},
            {"Expand",expand},
            {"Fast Backward",fast_backward},
            {"Fast Forward",fast_forward},
            {"Forward",forward},
            {"Pause",pause},
            {"Pause Circle",pause_circle},
            {"Pause Circle O",pause_circle_o},
            {"Play",play},
            {"Play Circle",play_circle},
            {"Play Circle O",play_circle_o},
            {"Random",random},
            {"Step Backward",step_backward},
            {"Step Forward",step_forward},
            {"Stop",stop},
            {"Stop Circle",stop_circle},
            {"Stop Circle O",stop_circle_o},
            {"Youtube Play",youtube_play},
        };
        public static Dictionary<string, string> BrandIcons = new Dictionary<string, string>()
        {
            {"Px500",px500},
            {"Adn",adn},
            {"Amazon",amazon},
            {"Android",android},
            {"Angellist",angellist},
            {"Apple",apple},
            {"Bandcamp",bandcamp},
            {"Behance",behance},
            {"Behance Square",behance_square},
            {"Bitbucket",bitbucket},
            {"Bitbucket Square",bitbucket_square},
            {"Bitcoin Alias",bitcoin_alias},
            {"Black Tie",black_tie},
            {"Bluetooth",bluetooth},
            {"Bluetooth B",bluetooth_b},
            {"Btc",btc},
            {"Buysellads",buysellads},
            {"Cc Amex",cc_amex},
            {"Cc Diners Club",cc_diners_club},
            {"Cc Discover",cc_discover},
            {"Cc Jcb",cc_jcb},
            {"Cc Mastercard",cc_mastercard},
            {"Cc Paypal",cc_paypal},
            {"Cc Stripe",cc_stripe},
            {"Cc Visa",cc_visa},
            {"Chrome",chrome},
            {"Codepen",codepen},
            {"Codiepie",codiepie},
            {"Connectdevelop",connectdevelop},
            {"Contao",contao},
            {"Css3",css3},
            {"Dashcube",dashcube},
            {"Delicious",delicious},
            {"Deviantart",deviantart},
            {"Digg",digg},
            {"Dribbble",dribbble},
            {"Dropbox",dropbox},
            {"Drupal",drupal},
            {"Edge",edge},
            {"Eercast",eercast},
            {"Empire",empire},
            {"Envira",envira},
            {"Etsy",etsy},
            {"Expeditedssl",expeditedssl},
            {"Fa Alias",fa_alias},
            {"Fonticons",fonticons},
            {"Fort Awesome",fort_awesome},
            {"Forumbee",forumbee},
            {"Foursquare",foursquare},
            {"Free Code Camp",free_code_camp},
            {"Ge Alias",ge_alias},
            {"Get Pocket",get_pocket},
            {"Gg",gg},
            {"Gg Circle",gg_circle},
            {"Git",git},
            {"Git Square",git_square},
            {"Github",github},
            {"Github Alt",github_alt},
            {"Github Square",github_square},
            {"Gitlab",gitlab},
            {"Gittip Alias",gittip_alias},
            {"Glide",glide},
            {"Glide G",glide_g},
            {"Google",google},
            {"Google Plus",google_plus},
            {"Google Plus Circle Alias",google_plus_circle_alias},
            {"Google Plus Official",google_plus_official},
            {"Google Plus Square",google_plus_square},
            {"Google Wallet",google_wallet},
            {"Gratipay",gratipay},
            {"Grav",grav},
            {"Hacker News",hacker_news},
            {"Houzz",houzz},
            {"Html5",html5},
            {"Imdb",imdb},
            {"Instagram",instagram},
            {"Internet Explorer",internet_explorer},
            {"Ioxhost",ioxhost},
            {"Joomla",joomla},
            {"Jsfiddle",jsfiddle},
            {"Lastfm Square",lastfm_square},
            {"Leanpub",leanpub},
            {"Linkedin",linkedin},
            {"Linkedin Square",linkedin_square},
            {"Linode",linode},
            {"Linux",linux},
            {"Maxcdn",maxcdn},
            {"Meanpath",meanpath},
            {"Medium",medium},
            {"Meetup",meetup},
            {"Mixcloud",mixcloud},
            {"Modx",modx},
            {"Odnoklassniki",odnoklassniki},
            {"Odnoklassniki Square",odnoklassniki_square},
            {"Opencart",opencart},
            {"Openid",openid},
            {"Opera",opera},
            {"Optin Monster",optin_monster},
            {"Pagelines",pagelines},
            {"Paypal",paypal},
            {"Pied Piper",pied_piper},
            {"Pied Piper Alt",pied_piper_alt},
            {"Pied Piper Pp",pied_piper_pp},
            {"Pinterest",pinterest},
            {"Pinterest P",pinterest_p},
            {"Pinterest Square",pinterest_square},
            {"Product Hunt",product_hunt},
            {"Qq",qq},
            {"Quora",quora},
            {"Ra Alias",ra_alias},
            {"Ravelry",ravelry},
            {"Rebel",rebel},
            {"Reddit",reddit},
            {"Reddit Alien",reddit_alien},
            {"Reddit Square",reddit_square},
            {"Renren",renren},
            {"Resistance Alias",resistance_alias},
            {"Safari",safari},
            {"Scribd",scribd},
            {"Sellsy",sellsy},
            {"Share Alt",share_alt},
            {"Share Alt Square",share_alt_square},
            {"Shirtsinbulk",shirtsinbulk},
            {"Simplybuilt",simplybuilt},
            {"Skyatlas",skyatlas},
            {"Skype",skype},
            {"Slack",slack},
            {"Slideshare",slideshare},
            {"Snapchat",snapchat},
            {"Snapchat Ghost",snapchat_ghost},
            {"Snapchat Square",snapchat_square},
            {"Soundcloud",soundcloud},
            {"Spotify",spotify},
            {"Stack Exchange",stack_exchange},
            {"Stack Overflow",stack_overflow},
            {"Steam",steam},
            {"Steam Square",steam_square},
            {"Stumbleupon",stumbleupon},
            {"Superpowers",superpowers},
            {"Telegram",telegram},
            {"Tencent Weibo",tencent_weibo},
            {"Trello",trello},
            {"Tripadvisor",tripadvisor},
            {"Tumblr",tumblr},
            {"Tumblr Square",tumblr_square},
            {"Twitch",twitch},
            {"Twitter",twitter},
            {"Twitter Square",twitter_square},
            {"Usb",usb},
            {"Viacoin",viacoin},
            {"Viadeo",viadeo},
            {"Viadeo Square",viadeo_square},
            {"Video Camera",video_camera},
            {"Vimeo",vimeo},
            {"Vimeo Square",vimeo_square},
            {"Vine",vine},
            {"Vk",vk},
            {"Wechat Alias",wechat_alias},
            {"Weibo",weibo},
            {"Weixin",weixin},
            {"Whatsapp",whatsapp},
            {"Wikipedia W",wikipedia_w},
            {"Windows",windows},
            {"Wordpress",wordpress},
            {"Wpbeginner",wpbeginner},
            {"Wpexplorer",wpexplorer},
            {"Wpforms",wpforms},
            {"Xing",xing},
            {"Xing Square",xing_square},
            {"Y Combinator",y_combinator},
            {"Y Combinator Square Alias",y_combinator_square_alias},
            {"Yahoo",yahoo},
            {"Yc Alias",yc_alias},
            {"Yc Square Alias",yc_square_alias},
            {"Yelp",yelp},
            {"Yen Alias",yen_alias},
            {"Yoast",yoast},
            {"Youtube",youtube},
            {"Youtube Play",youtube_play},
            {"Youtube Square",youtube_square},
        };
        public static Dictionary<string, string> MedicalIcons = new Dictionary<string, string>()
        {
            {"Ambulance",ambulance},
            {"H Square",h_square},
            {"Heart",heart},
            {"Heart O",heart_o},
            {"Heartbeat",heartbeat},
            {"Hospital O",hospital_o},
            {"Medkit",medkit},
            {"Plus Square",plus_square},
            {"Stethoscope",stethoscope},
            {"User Md",user_md},
            {"Wheelchair",wheelchair},
            {"Wheelchair Alt",wheelchair_alt},
        };

        #region Character Codes
        public const string px500 = "\uf26e";
        public const string address_book = "\uf2b9";
        public const string address_book_o = "\uf2ba";
        public const string address_card = "\uf2bb";
        public const string address_card_o = "\uf2bc";
        public const string adjust = "\uf042";
        public const string adn = "\uf170";
        public const string align_center = "\uf037";
        public const string align_justify = "\uf039";
        public const string align_left = "\uf036";
        public const string align_right = "\uf038";
        public const string amazon = "\uf270";
        public const string ambulance = "\uf0f9";
        public const string american_sign_language_interpreting = "\uf2a3";
        public const string anchor = "\uf13d";
        public const string android = "\uf17b";
        public const string angellist = "\uf209";
        public const string angle_double_down = "\uf103";
        public const string angle_double_left = "\uf100";
        public const string angle_double_right = "\uf101";
        public const string angle_double_up = "\uf102";
        public const string angle_down = "\uf107";
        public const string angle_left = "\uf104";
        public const string angle_right = "\uf105";
        public const string angle_up = "\uf106";
        public const string apple = "\uf179";
        public const string archive = "\uf187";
        public const string area_chart = "\uf1fe";
        public const string arrow_circle_down = "\uf0ab";
        public const string arrow_circle_left = "\uf0a8";
        public const string arrow_circle_o_down = "\uf01a";
        public const string arrow_circle_o_left = "\uf190";
        public const string arrow_circle_o_right = "\uf18e";
        public const string arrow_circle_o_up = "\uf01b";
        public const string arrow_circle_right = "\uf0a9";
        public const string arrow_circle_up = "\uf0aa";
        public const string arrow_down = "\uf063";
        public const string arrow_left = "\uf060";
        public const string arrow_right = "\uf061";
        public const string arrow_up = "\uf062";
        public const string arrows = "\uf047";
        public const string arrows_alt = "\uf0b2";
        public const string arrows_h = "\uf07e";
        public const string arrows_v = "\uf07d";
        public const string asl_interpreting_alias = "\uf2a3";
        public const string assistive_listening_systems = "\uf2a2";
        public const string asterisk = "\uf069";
        public const string at = "\uf1fa";
        public const string audio_description = "\uf29e";
        public const string automobile_alias = "\uf1b9";
        public const string backward = "\uf04a";
        public const string balance_scale = "\uf24e";
        public const string ban = "\uf05e";
        public const string bandcamp = "\uf2d5";
        public const string bank_alias = "\uf19c";
        public const string bar_chart = "\uf080";
        public const string bar_chart_o_alias = "\uf080";
        public const string barcode = "\uf02a";
        public const string bars = "\uf0c9";
        public const string bath = "\uf2cd";
        public const string bathtub_alias = "\uf2cd";
        public const string battery_alias = "\uf240";
        public const string battery_0_alias = "\uf244";
        public const string battery_1_alias = "\uf243";
        public const string battery_2_alias = "\uf242";
        public const string battery_3_alias = "\uf241";
        public const string battery_4_alias = "\uf240";
        public const string battery_empty = "\uf244";
        public const string battery_full = "\uf240";
        public const string battery_half = "\uf242";
        public const string battery_quarter = "\uf243";
        public const string battery_three_quarters = "\uf241";
        public const string bed = "\uf236";
        public const string beer = "\uf0fc";
        public const string behance = "\uf1b4";
        public const string behance_square = "\uf1b5";
        public const string bell = "\uf0f3";
        public const string bell_o = "\uf0a2";
        public const string bell_slash = "\uf1f6";
        public const string bell_slash_o = "\uf1f7";
        public const string bicycle = "\uf206";
        public const string binoculars = "\uf1e5";
        public const string birthday_cake = "\uf1fd";
        public const string bitbucket = "\uf171";
        public const string bitbucket_square = "\uf172";
        public const string bitcoin_alias = "\uf15a";
        public const string black_tie = "\uf27e";
        public const string blind = "\uf29d";
        public const string bluetooth = "\uf293";
        public const string bluetooth_b = "\uf294";
        public const string bold = "\uf032";
        public const string bolt = "\uf0e7";
        public const string bomb = "\uf1e2";
        public const string book = "\uf02d";
        public const string bookmark = "\uf02e";
        public const string bookmark_o = "\uf097";
        public const string braille = "\uf2a1";
        public const string briefcase = "\uf0b1";
        public const string btc = "\uf15a";
        public const string bug = "\uf188";
        public const string building = "\uf1ad";
        public const string building_o = "\uf0f7";
        public const string bullhorn = "\uf0a1";
        public const string bullseye = "\uf140";
        public const string bus = "\uf207";
        public const string buysellads = "\uf20d";
        public const string cab_alias = "\uf1ba";
        public const string calculator = "\uf1ec";
        public const string calendar = "\uf073";
        public const string calendar_check_o = "\uf274";
        public const string calendar_minus_o = "\uf272";
        public const string calendar_o = "\uf133";
        public const string calendar_plus_o = "\uf271";
        public const string calendar_times_o = "\uf273";
        public const string camera = "\uf030";
        public const string camera_retro = "\uf083";
        public const string car = "\uf1b9";
        public const string caret_down = "\uf0d7";
        public const string caret_left = "\uf0d9";
        public const string caret_right = "\uf0da";
        public const string caret_square_o_down = "\uf150";
        public const string caret_square_o_left = "\uf191";
        public const string caret_square_o_right = "\uf152";
        public const string caret_square_o_up = "\uf151";
        public const string caret_up = "\uf0d8";
        public const string cart_arrow_down = "\uf218";
        public const string cart_plus = "\uf217";
        public const string cc = "\uf20a";
        public const string cc_amex = "\uf1f3";
        public const string cc_diners_club = "\uf24c";
        public const string cc_discover = "\uf1f2";
        public const string cc_jcb = "\uf24b";
        public const string cc_mastercard = "\uf1f1";
        public const string cc_paypal = "\uf1f4";
        public const string cc_stripe = "\uf1f5";
        public const string cc_visa = "\uf1f0";
        public const string certificate = "\uf0a3";
        public const string chain_alias = "\uf0c1";
        public const string chain_broken = "\uf127";
        public const string check = "\uf00c";
        public const string check_circle = "\uf058";
        public const string check_circle_o = "\uf05d";
        public const string check_square = "\uf14a";
        public const string check_square_o = "\uf046";
        public const string chevron_circle_down = "\uf13a";
        public const string chevron_circle_left = "\uf137";
        public const string chevron_circle_right = "\uf138";
        public const string chevron_circle_up = "\uf139";
        public const string chevron_down = "\uf078";
        public const string chevron_left = "\uf053";
        public const string chevron_right = "\uf054";
        public const string chevron_up = "\uf077";
        public const string child = "\uf1ae";
        public const string chrome = "\uf268";
        public const string circle = "\uf111";
        public const string circle_o = "\uf10c";
        public const string circle_o_notch = "\uf1ce";
        public const string circle_thin = "\uf1db";
        public const string clipboard = "\uf0ea";
        public const string clock_o = "\uf017";
        public const string clone = "\uf24d";
        public const string close_alias = "\uf00d";
        public const string cloud = "\uf0c2";
        public const string cloud_download = "\uf0ed";
        public const string cloud_upload = "\uf0ee";
        public const string cny_alias = "\uf157";
        public const string code = "\uf121";
        public const string code_fork = "\uf126";
        public const string codepen = "\uf1cb";
        public const string codiepie = "\uf284";
        public const string coffee = "\uf0f4";
        public const string cog = "\uf013";
        public const string cogs = "\uf085";
        public const string columns = "\uf0db";
        public const string comment = "\uf075";
        public const string comment_o = "\uf0e5";
        public const string commenting = "\uf27a";
        public const string commenting_o = "\uf27b";
        public const string comments = "\uf086";
        public const string comments_o = "\uf0e6";
        public const string compass = "\uf14e";
        public const string compress = "\uf066";
        public const string connectdevelop = "\uf20e";
        public const string contao = "\uf26d";
        public const string copy_alias = "\uf0c5";
        public const string copyright = "\uf1f9";
        public const string creative_commons = "\uf25e";
        public const string credit_card = "\uf09d";
        public const string credit_card_alt = "\uf283";
        public const string crop = "\uf125";
        public const string crosshairs = "\uf05b";
        public const string css3 = "\uf13c";
        public const string cube = "\uf1b2";
        public const string cubes = "\uf1b3";
        public const string cut_alias = "\uf0c4";
        public const string cutlery = "\uf0f5";
        public const string dashboard_alias = "\uf0e4";
        public const string dashcube = "\uf210";
        public const string database = "\uf1c0";
        public const string deaf = "\uf2a4";
        public const string deafness_alias = "\uf2a4";
        public const string dedent_alias = "\uf03b";
        public const string delicious = "\uf1a5";
        public const string desktop = "\uf108";
        public const string deviantart = "\uf1bd";
        public const string diamond = "\uf219";
        public const string digg = "\uf1a6";
        public const string dollar_alias = "\uf155";
        public const string dot_circle_o = "\uf192";
        public const string download = "\uf019";
        public const string dribbble = "\uf17d";
        public const string drivers_license_alias = "\uf2c2";
        public const string drivers_license_o_alias = "\uf2c3";
        public const string dropbox = "\uf16b";
        public const string drupal = "\uf1a9";
        public const string edge = "\uf282";
        public const string edit_alias = "\uf044";
        public const string eercast = "\uf2da";
        public const string eject = "\uf052";
        public const string ellipsis_h = "\uf141";
        public const string ellipsis_v = "\uf142";
        public const string empire = "\uf1d1";
        public const string envelope = "\uf0e0";
        public const string envelope_o = "\uf003";
        public const string envelope_open = "\uf2b6";
        public const string envelope_open_o = "\uf2b7";
        public const string envelope_square = "\uf199";
        public const string envira = "\uf299";
        public const string eraser = "\uf12d";
        public const string etsy = "\uf2d7";
        public const string eur = "\uf153";
        public const string euro_alias = "\uf153";
        public const string exchange = "\uf0ec";
        public const string exclamation = "\uf12a";
        public const string exclamation_circle = "\uf06a";
        public const string exclamation_triangle = "\uf071";
        public const string expand = "\uf065";
        public const string expeditedssl = "\uf23e";
        public const string external_link = "\uf08e";
        public const string external_link_square = "\uf14c";
        public const string eye = "\uf06e";
        public const string eye_slash = "\uf070";
        public const string eyedropper = "\uf1fb";
        public const string fa_alias = "\uf2b4";
        public const string facebook = "\uf09a";
        public const string facebook_f_alias = "\uf09a";
        public const string facebook_official = "\uf230";
        public const string facebook_square = "\uf082";
        public const string fast_backward = "\uf049";
        public const string fast_forward = "\uf050";
        public const string fax = "\uf1ac";
        public const string feed_alias = "\uf09e";
        public const string female = "\uf182";
        public const string fighter_jet = "\uf0fb";
        public const string file = "\uf15b";
        public const string file_archive_o = "\uf1c6";
        public const string file_audio_o = "\uf1c7";
        public const string file_code_o = "\uf1c9";
        public const string file_excel_o = "\uf1c3";
        public const string file_image_o = "\uf1c5";
        public const string file_movie_o_alias = "\uf1c8";
        public const string file_o = "\uf016";
        public const string file_pdf_o = "\uf1c1";
        public const string file_photo_o_alias = "\uf1c5";
        public const string file_picture_o_alias = "\uf1c5";
        public const string file_powerpoint_o = "\uf1c4";
        public const string file_sound_o_alias = "\uf1c7";
        public const string file_text = "\uf15c";
        public const string file_text_o = "\uf0f6";
        public const string file_video_o = "\uf1c8";
        public const string file_word_o = "\uf1c2";
        public const string file_zip_o_alias = "\uf1c6";
        public const string files_o = "\uf0c5";
        public const string film = "\uf008";
        public const string filter = "\uf0b0";
        public const string fire = "\uf06d";
        public const string fire_extinguisher = "\uf134";
        public const string firefox = "\uf269";
        public const string first_order = "\uf2b0";
        public const string flag = "\uf024";
        public const string flag_checkered = "\uf11e";
        public const string flag_o = "\uf11d";
        public const string flash_alias = "\uf0e7";
        public const string flask = "\uf0c3";
        public const string flickr = "\uf16e";
        public const string floppy_o = "\uf0c7";
        public const string folder = "\uf07b";
        public const string folder_o = "\uf114";
        public const string folder_open = "\uf07c";
        public const string folder_open_o = "\uf115";
        public const string font = "\uf031";
        public const string font_awesome = "\uf2b4";
        public const string fonticons = "\uf280";
        public const string fort_awesome = "\uf286";
        public const string forumbee = "\uf211";
        public const string forward = "\uf04e";
        public const string foursquare = "\uf180";
        public const string free_code_camp = "\uf2c5";
        public const string frown_o = "\uf119";
        public const string futbol_o = "\uf1e3";
        public const string gamepad = "\uf11b";
        public const string gavel = "\uf0e3";
        public const string gbp = "\uf154";
        public const string ge_alias = "\uf1d1";
        public const string gear_alias = "\uf013";
        public const string gears_alias = "\uf085";
        public const string genderless = "\uf22d";
        public const string get_pocket = "\uf265";
        public const string gg = "\uf260";
        public const string gg_circle = "\uf261";
        public const string gift = "\uf06b";
        public const string git = "\uf1d3";
        public const string git_square = "\uf1d2";
        public const string github = "\uf09b";
        public const string github_alt = "\uf113";
        public const string github_square = "\uf092";
        public const string gitlab = "\uf296";
        public const string gittip_alias = "\uf184";
        public const string glass = "\uf000";
        public const string glide = "\uf2a5";
        public const string glide_g = "\uf2a6";
        public const string globe = "\uf0ac";
        public const string google = "\uf1a0";
        public const string google_plus = "\uf0d5";
        public const string google_plus_circle_alias = "\uf2b3";
        public const string google_plus_official = "\uf2b3";
        public const string google_plus_square = "\uf0d4";
        public const string google_wallet = "\uf1ee";
        public const string graduation_cap = "\uf19d";
        public const string gratipay = "\uf184";
        public const string grav = "\uf2d6";
        public const string group_alias = "\uf0c0";
        public const string h_square = "\uf0fd";
        public const string hacker_news = "\uf1d4";
        public const string hand_grab_o_alias = "\uf255";
        public const string hand_lizard_o = "\uf258";
        public const string hand_o_down = "\uf0a7";
        public const string hand_o_left = "\uf0a5";
        public const string hand_o_right = "\uf0a4";
        public const string hand_o_up = "\uf0a6";
        public const string hand_paper_o = "\uf256";
        public const string hand_peace_o = "\uf25b";
        public const string hand_pointer_o = "\uf25a";
        public const string hand_rock_o = "\uf255";
        public const string hand_scissors_o = "\uf257";
        public const string hand_spock_o = "\uf259";
        public const string hand_stop_o_alias = "\uf256";
        public const string handshake_o = "\uf2b5";
        public const string hard_of_hearing_alias = "\uf2a4";
        public const string hashtag = "\uf292";
        public const string hdd_o = "\uf0a0";
        public const string header = "\uf1dc";
        public const string headphones = "\uf025";
        public const string heart = "\uf004";
        public const string heart_o = "\uf08a";
        public const string heartbeat = "\uf21e";
        public const string history = "\uf1da";
        public const string home = "\uf015";
        public const string hospital_o = "\uf0f8";
        public const string hotel_alias = "\uf236";
        public const string hourglass = "\uf254";
        public const string hourglass_1_alias = "\uf251";
        public const string hourglass_2_alias = "\uf252";
        public const string hourglass_3_alias = "\uf253";
        public const string hourglass_end = "\uf253";
        public const string hourglass_half = "\uf252";
        public const string hourglass_o = "\uf250";
        public const string hourglass_start = "\uf251";
        public const string houzz = "\uf27c";
        public const string html5 = "\uf13b";
        public const string i_cursor = "\uf246";
        public const string id_badge = "\uf2c1";
        public const string id_card = "\uf2c2";
        public const string id_card_o = "\uf2c3";
        public const string ils = "\uf20b";
        public const string image_alias = "\uf03e";
        public const string imdb = "\uf2d8";
        public const string inbox = "\uf01c";
        public const string indent = "\uf03c";
        public const string industry = "\uf275";
        public const string info = "\uf129";
        public const string info_circle = "\uf05a";
        public const string inr = "\uf156";
        public const string instagram = "\uf16d";
        public const string institution_alias = "\uf19c";
        public const string internet_explorer = "\uf26b";
        public const string intersex_alias = "\uf224";
        public const string ioxhost = "\uf208";
        public const string italic = "\uf033";
        public const string joomla = "\uf1aa";
        public const string jpy = "\uf157";
        public const string jsfiddle = "\uf1cc";
        public const string key = "\uf084";
        public const string keyboard_o = "\uf11c";
        public const string krw = "\uf159";
        public const string language = "\uf1ab";
        public const string laptop = "\uf109";
        public const string lastfm = "\uf202";
        public const string lastfm_square = "\uf203";
        public const string leaf = "\uf06c";
        public const string leanpub = "\uf212";
        public const string legal_alias = "\uf0e3";
        public const string lemon_o = "\uf094";
        public const string level_down = "\uf149";
        public const string level_up = "\uf148";
        public const string life_bouy_alias = "\uf1cd";
        public const string life_buoy_alias = "\uf1cd";
        public const string life_ring = "\uf1cd";
        public const string life_saver_alias = "\uf1cd";
        public const string lightbulb_o = "\uf0eb";
        public const string line_chart = "\uf201";
        public const string link = "\uf0c1";
        public const string linkedin = "\uf0e1";
        public const string linkedin_square = "\uf08c";
        public const string linode = "\uf2b8";
        public const string linux = "\uf17c";
        public const string list = "\uf03a";
        public const string list_alt = "\uf022";
        public const string list_ol = "\uf0cb";
        public const string list_ul = "\uf0ca";
        public const string location_arrow = "\uf124";
        public const string lock_ = "\uf023";
        public const string long_arrow_down = "\uf175";
        public const string long_arrow_left = "\uf177";
        public const string long_arrow_right = "\uf178";
        public const string long_arrow_up = "\uf176";
        public const string low_vision = "\uf2a8";
        public const string magic = "\uf0d0";
        public const string magnet = "\uf076";
        public const string mail_forward_alias = "\uf064";
        public const string mail_reply_alias = "\uf112";
        public const string mail_reply_all_alias = "\uf122";
        public const string male = "\uf183";
        public const string map = "\uf279";
        public const string map_marker = "\uf041";
        public const string map_o = "\uf278";
        public const string map_pin = "\uf276";
        public const string map_signs = "\uf277";
        public const string mars = "\uf222";
        public const string mars_double = "\uf227";
        public const string mars_stroke = "\uf229";
        public const string mars_stroke_h = "\uf22b";
        public const string mars_stroke_v = "\uf22a";
        public const string maxcdn = "\uf136";
        public const string meanpath = "\uf20c";
        public const string medium = "\uf23a";
        public const string medkit = "\uf0fa";
        public const string meetup = "\uf2e0";
        public const string meh_o = "\uf11a";
        public const string mercury = "\uf223";
        public const string microchip = "\uf2db";
        public const string microphone = "\uf130";
        public const string microphone_slash = "\uf131";
        public const string minus = "\uf068";
        public const string minus_circle = "\uf056";
        public const string minus_square = "\uf146";
        public const string minus_square_o = "\uf147";
        public const string mixcloud = "\uf289";
        public const string mobile = "\uf10b";
        public const string mobile_phone_alias = "\uf10b";
        public const string modx = "\uf285";
        public const string money = "\uf0d6";
        public const string moon_o = "\uf186";
        public const string mortar_board_alias = "\uf19d";
        public const string motorcycle = "\uf21c";
        public const string mouse_pointer = "\uf245";
        public const string music = "\uf001";
        public const string navicon_alias = "\uf0c9";
        public const string neuter = "\uf22c";
        public const string newspaper_o = "\uf1ea";
        public const string object_group = "\uf247";
        public const string object_ungroup = "\uf248";
        public const string odnoklassniki = "\uf263";
        public const string odnoklassniki_square = "\uf264";
        public const string opencart = "\uf23d";
        public const string openid = "\uf19b";
        public const string opera = "\uf26a";
        public const string optin_monster = "\uf23c";
        public const string outdent = "\uf03b";
        public const string pagelines = "\uf18c";
        public const string paint_brush = "\uf1fc";
        public const string paper_plane = "\uf1d8";
        public const string paper_plane_o = "\uf1d9";
        public const string paperclip = "\uf0c6";
        public const string paragraph = "\uf1dd";
        public const string paste_alias = "\uf0ea";
        public const string pause = "\uf04c";
        public const string pause_circle = "\uf28b";
        public const string pause_circle_o = "\uf28c";
        public const string paw = "\uf1b0";
        public const string paypal = "\uf1ed";
        public const string pencil = "\uf040";
        public const string pencil_square = "\uf14b";
        public const string pencil_square_o = "\uf044";
        public const string percent = "\uf295";
        public const string phone = "\uf095";
        public const string phone_square = "\uf098";
        public const string photo_alias = "\uf03e";
        public const string picture_o = "\uf03e";
        public const string pie_chart = "\uf200";
        public const string pied_piper = "\uf2ae";
        public const string pied_piper_alt = "\uf1a8";
        public const string pied_piper_pp = "\uf1a7";
        public const string pinterest = "\uf0d2";
        public const string pinterest_p = "\uf231";
        public const string pinterest_square = "\uf0d3";
        public const string plane = "\uf072";
        public const string play = "\uf04b";
        public const string play_circle = "\uf144";
        public const string play_circle_o = "\uf01d";
        public const string plug = "\uf1e6";
        public const string plus = "\uf067";
        public const string plus_circle = "\uf055";
        public const string plus_square = "\uf0fe";
        public const string plus_square_o = "\uf196";
        public const string podcast = "\uf2ce";
        public const string power_off = "\uf011";
        public const string print = "\uf02f";
        public const string product_hunt = "\uf288";
        public const string puzzle_piece = "\uf12e";
        public const string qq = "\uf1d6";
        public const string qrcode = "\uf029";
        public const string question = "\uf128";
        public const string question_circle = "\uf059";
        public const string question_circle_o = "\uf29c";
        public const string quora = "\uf2c4";
        public const string quote_left = "\uf10d";
        public const string quote_right = "\uf10e";
        public const string ra_alias = "\uf1d0";
        public const string random = "\uf074";
        public const string ravelry = "\uf2d9";
        public const string rebel = "\uf1d0";
        public const string recycle = "\uf1b8";
        public const string reddit = "\uf1a1";
        public const string reddit_alien = "\uf281";
        public const string reddit_square = "\uf1a2";
        public const string refresh = "\uf021";
        public const string registered = "\uf25d";
        public const string remove_alias = "\uf00d";
        public const string renren = "\uf18b";
        public const string reorder_alias = "\uf0c9";
        public const string repeat = "\uf01e";
        public const string reply = "\uf112";
        public const string reply_all = "\uf122";
        public const string resistance_alias = "\uf1d0";
        public const string retweet = "\uf079";
        public const string rmb_alias = "\uf157";
        public const string road = "\uf018";
        public const string rocket = "\uf135";
        public const string rotate_left_alias = "\uf0e2";
        public const string rotate_right_alias = "\uf01e";
        public const string rouble_alias = "\uf158";
        public const string rss = "\uf09e";
        public const string rss_square = "\uf143";
        public const string rub = "\uf158";
        public const string ruble_alias = "\uf158";
        public const string rupee_alias = "\uf156";
        public const string s15_alias = "\uf2cd";
        public const string safari = "\uf267";
        public const string save_alias = "\uf0c7";
        public const string scissors = "\uf0c4";
        public const string scribd = "\uf28a";
        public const string search = "\uf002";
        public const string search_minus = "\uf010";
        public const string search_plus = "\uf00e";
        public const string sellsy = "\uf213";
        public const string send_alias = "\uf1d8";
        public const string send_o_alias = "\uf1d9";
        public const string server = "\uf233";
        public const string share = "\uf064";
        public const string share_alt = "\uf1e0";
        public const string share_alt_square = "\uf1e1";
        public const string share_square = "\uf14d";
        public const string share_square_o = "\uf045";
        public const string shekel_alias = "\uf20b";
        public const string sheqel_alias = "\uf20b";
        public const string shield = "\uf132";
        public const string ship = "\uf21a";
        public const string shirtsinbulk = "\uf214";
        public const string shopping_bag = "\uf290";
        public const string shopping_basket = "\uf291";
        public const string shopping_cart = "\uf07a";
        public const string shower = "\uf2cc";
        public const string sign_in = "\uf090";
        public const string sign_language = "\uf2a7";
        public const string sign_out = "\uf08b";
        public const string signal = "\uf012";
        public const string signing_alias = "\uf2a7";
        public const string simplybuilt = "\uf215";
        public const string sitemap = "\uf0e8";
        public const string skyatlas = "\uf216";
        public const string skype = "\uf17e";
        public const string slack = "\uf198";
        public const string sliders = "\uf1de";
        public const string slideshare = "\uf1e7";
        public const string smile_o = "\uf118";
        public const string snapchat = "\uf2ab";
        public const string snapchat_ghost = "\uf2ac";
        public const string snapchat_square = "\uf2ad";
        public const string snowflake_o = "\uf2dc";
        public const string soccer_ball_o_alias = "\uf1e3";
        public const string sort = "\uf0dc";
        public const string sort_alpha_asc = "\uf15d";
        public const string sort_alpha_desc = "\uf15e";
        public const string sort_amount_asc = "\uf160";
        public const string sort_amount_desc = "\uf161";
        public const string sort_asc = "\uf0de";
        public const string sort_desc = "\uf0dd";
        public const string sort_down_alias = "\uf0dd";
        public const string sort_numeric_asc = "\uf162";
        public const string sort_numeric_desc = "\uf163";
        public const string sort_up_alias = "\uf0de";
        public const string soundcloud = "\uf1be";
        public const string space_shuttle = "\uf197";
        public const string spinner = "\uf110";
        public const string spoon = "\uf1b1";
        public const string spotify = "\uf1bc";
        public const string square = "\uf0c8";
        public const string square_o = "\uf096";
        public const string stack_exchange = "\uf18d";
        public const string stack_overflow = "\uf16c";
        public const string star = "\uf005";
        public const string star_half = "\uf089";
        public const string star_half_empty_alias = "\uf123";
        public const string star_half_full_alias = "\uf123";
        public const string star_half_o = "\uf123";
        public const string star_o = "\uf006";
        public const string steam = "\uf1b6";
        public const string steam_square = "\uf1b7";
        public const string step_backward = "\uf048";
        public const string step_forward = "\uf051";
        public const string stethoscope = "\uf0f1";
        public const string sticky_note = "\uf249";
        public const string sticky_note_o = "\uf24a";
        public const string stop = "\uf04d";
        public const string stop_circle = "\uf28d";
        public const string stop_circle_o = "\uf28e";
        public const string street_view = "\uf21d";
        public const string strikethrough = "\uf0cc";
        public const string stumbleupon = "\uf1a4";
        public const string stumbleupon_circle = "\uf1a3";
        public const string subscript = "\uf12c";
        public const string subway = "\uf239";
        public const string suitcase = "\uf0f2";
        public const string sun_o = "\uf185";
        public const string superpowers = "\uf2dd";
        public const string superscript = "\uf12b";
        public const string support_alias = "\uf1cd";
        public const string table = "\uf0ce";
        public const string tablet = "\uf10a";
        public const string tachometer = "\uf0e4";
        public const string tag = "\uf02b";
        public const string tags = "\uf02c";
        public const string tasks = "\uf0ae";
        public const string taxi = "\uf1ba";
        public const string telegram = "\uf2c6";
        public const string television = "\uf26c";
        public const string tencent_weibo = "\uf1d5";
        public const string terminal = "\uf120";
        public const string text_height = "\uf034";
        public const string text_width = "\uf035";
        public const string th = "\uf00a";
        public const string th_large = "\uf009";
        public const string th_list = "\uf00b";
        public const string themeisle = "\uf2b2";
        public const string thermometer_alias = "\uf2c7";
        public const string thermometer_0_alias = "\uf2cb";
        public const string thermometer_1_alias = "\uf2ca";
        public const string thermometer_2_alias = "\uf2c9";
        public const string thermometer_3_alias = "\uf2c8";
        public const string thermometer_4_alias = "\uf2c7";
        public const string thermometer_empty = "\uf2cb";
        public const string thermometer_full = "\uf2c7";
        public const string thermometer_half = "\uf2c9";
        public const string thermometer_quarter = "\uf2ca";
        public const string thermometer_three_quarters = "\uf2c8";
        public const string thumb_tack = "\uf08d";
        public const string thumbs_down = "\uf165";
        public const string thumbs_o_down = "\uf088";
        public const string thumbs_o_up = "\uf087";
        public const string thumbs_up = "\uf164";
        public const string ticket = "\uf145";
        public const string times = "\uf00d";
        public const string times_circle = "\uf057";
        public const string times_circle_o = "\uf05c";
        public const string times_rectangle_alias = "\uf2d3";
        public const string times_rectangle_o_alias = "\uf2d4";
        public const string tint = "\uf043";
        public const string toggle_down_alias = "\uf150";
        public const string toggle_left_alias = "\uf191";
        public const string toggle_off = "\uf204";
        public const string toggle_on = "\uf205";
        public const string toggle_right_alias = "\uf152";
        public const string toggle_up_alias = "\uf151";
        public const string trademark = "\uf25c";
        public const string train = "\uf238";
        public const string transgender = "\uf224";
        public const string transgender_alt = "\uf225";
        public const string trash = "\uf1f8";
        public const string trash_o = "\uf014";
        public const string tree = "\uf1bb";
        public const string trello = "\uf181";
        public const string tripadvisor = "\uf262";
        public const string trophy = "\uf091";
        public const string truck = "\uf0d1";
        public const string try_ = "\uf195";
        public const string tty = "\uf1e4";
        public const string tumblr = "\uf173";
        public const string tumblr_square = "\uf174";
        public const string turkish_lira_alias = "\uf195";
        public const string tv_alias = "\uf26c";
        public const string twitch = "\uf1e8";
        public const string twitter = "\uf099";
        public const string twitter_square = "\uf081";
        public const string umbrella = "\uf0e9";
        public const string underline = "\uf0cd";
        public const string undo = "\uf0e2";
        public const string universal_access = "\uf29a";
        public const string university = "\uf19c";
        public const string unlink_alias = "\uf127";
        public const string unlock = "\uf09c";
        public const string unlock_alt = "\uf13e";
        public const string unsorted_alias = "\uf0dc";
        public const string upload = "\uf093";
        public const string usb = "\uf287";
        public const string usd = "\uf155";
        public const string user = "\uf007";
        public const string user_circle = "\uf2bd";
        public const string user_circle_o = "\uf2be";
        public const string user_md = "\uf0f0";
        public const string user_o = "\uf2c0";
        public const string user_plus = "\uf234";
        public const string user_secret = "\uf21b";
        public const string user_times = "\uf235";
        public const string users = "\uf0c0";
        public const string vcard_alias = "\uf2bb";
        public const string vcard_o_alias = "\uf2bc";
        public const string venus = "\uf221";
        public const string venus_double = "\uf226";
        public const string venus_mars = "\uf228";
        public const string viacoin = "\uf237";
        public const string viadeo = "\uf2a9";
        public const string viadeo_square = "\uf2aa";
        public const string video_camera = "\uf03d";
        public const string vimeo = "\uf27d";
        public const string vimeo_square = "\uf194";
        public const string vine = "\uf1ca";
        public const string vk = "\uf189";
        public const string volume_control_phone = "\uf2a0";
        public const string volume_down = "\uf027";
        public const string volume_off = "\uf026";
        public const string volume_up = "\uf028";
        public const string warning_alias = "\uf071";
        public const string wechat_alias = "\uf1d7";
        public const string weibo = "\uf18a";
        public const string weixin = "\uf1d7";
        public const string whatsapp = "\uf232";
        public const string wheelchair = "\uf193";
        public const string wheelchair_alt = "\uf29b";
        public const string wifi = "\uf1eb";
        public const string wikipedia_w = "\uf266";
        public const string window_close = "\uf2d3";
        public const string window_close_o = "\uf2d4";
        public const string window_maximize = "\uf2d0";
        public const string window_minimize = "\uf2d1";
        public const string window_restore = "\uf2d2";
        public const string windows = "\uf17a";
        public const string won_alias = "\uf159";
        public const string wordpress = "\uf19a";
        public const string wpbeginner = "\uf297";
        public const string wpexplorer = "\uf2de";
        public const string wpforms = "\uf298";
        public const string wrench = "\uf0ad";
        public const string xing = "\uf168";
        public const string xing_square = "\uf169";
        public const string y_combinator = "\uf23b";
        public const string y_combinator_square_alias = "\uf1d4";
        public const string yahoo = "\uf19e";
        public const string yc_alias = "\uf23b";
        public const string yc_square_alias = "\uf1d4";
        public const string yelp = "\uf1e9";
        public const string yen_alias = "\uf157";
        public const string yoast = "\uf2b1";
        public const string youtube = "\uf167";
        public const string youtube_play = "\uf16a";
        public const string youtube_square = "\uf166";
        #endregion

        private static Font _font;
        /// <summary>
        /// Returns the FontAwesome reference.
        /// </summary>
        public static Font Font
        {
            get
            {
                if (_font == null) { _font = Resources.Load("Quick/Fonts/FontAwesome") as Font; }
                return _font;
            }
        }
    }
}
